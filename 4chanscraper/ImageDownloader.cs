using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using System.Threading;

namespace Scraper
{
	public class ImageDownloader : IDisposable
	{
		private Thread[] workers;
		private Queue<Pair<WaitCallback, object>> work;
		private ManualResetEvent mre = new ManualResetEvent(false);
		private DateTime start;
		private long bytesDownloaded;
		private int jobsQueued, jobsFinished;

		private bool disposing = false;

		public int DownloadThreads
		{
			get { if (this.workers == null) throw new ObjectDisposedException("ImageDownloader");  return this.workers.Length; }
			set
			{
				if (value < 1) return;
				if (this.workers == null) throw new ObjectDisposedException("ImageDownloader"); 
				Thread[] workers = new Thread[value];

				for (int i = value; i < this.workers.Length; i++) // Terminate un-needed threads.
					this.workers[i].Abort();

				for (int i = 0; i < this.workers.Length && i < workers.Length; i++) // Copy existing threads.
					workers[i] = this.workers[i];

				for (int i = this.workers.Length; i < workers.Length; i++) // Create new workers as necessary.
					workers[i] = _createWorker();

				this.workers = workers;
			}
		}

		public string DownloadSpeed
		{
			get { return Math.Round((bytesDownloaded / 1024.0) / ((DateTime.Now - this.start).TotalSeconds), 2) + " KB/s"; }
		}

		public int QueueLength
		{
			get { if (this.workers == null) throw new ObjectDisposedException("ImageDownloader"); if (this.jobsQueued == this.jobsFinished) this.jobsFinished = this.jobsQueued = 0; return this.jobsQueued - this.jobsFinished; }
		}

		public ImageDownloader(int size)
		{
			this.work = new Queue<Pair<WaitCallback, object>>();

			this.workers = new Thread[size];
			for (int i = 0; i < size; i++){
				this.workers[i] = _createWorker();
			}

			this.start = DateTime.Now;
			this.bytesDownloaded = 0;
			this.jobsFinished = this.jobsQueued = 0;
		}

		public void QueuePost(string name, Data.Post p)
		{
			Pair<WaitCallback, object> pair = new Pair<WaitCallback, object>(
				new WaitCallback(this._downloadPostImage),
				new Pair<string, Data.Post>(name, p));

			if (this.workers == null) throw new ObjectDisposedException("ImageDownloader"); 

			lock (this.work)
			{
				this.work.Enqueue(pair);
				this.jobsQueued++;
			}

			this.mre.Set();
		}

		public void Dispose()
		{
			lock (this.workers)
			{
				if (this.disposing)
					return;

				for (int i = 0; i < this.workers.Length; i++)
					this.workers[i].Abort();

				this.disposing = true;

				this.workers = null;
				this.work = null;
			}
		}

		private void _downloadPostImage(object o)
		{
			if (o.GetType() != typeof(Pair<string, Data.Post>))
			{
				DebugConsole.ShowWarning("Download thread passed object that was not a post; bug?");
				return;
			}

			Pair<string, Data.Post> p = (Pair<string, Data.Post>) o;
			p.Left = Path.GetFullPath(p.Left);
			try
			{
				using (WebClient wc = new WebClient())
				{
					frmMain.__UpdateProgessCallback upc = new frmMain.__UpdateProgessCallback(Program.mainForm.UpdateStatusStripProgress);
					frmMain.__UpdateStatusText ust = new frmMain.__UpdateStatusText(Program.mainForm.UpdateStatusStripText);
					this.bytesDownloaded = 0; this.start = DateTime.Now;

					if (File.Exists(p.Left))
					{ // Assume already downloaded.
						DebugConsole.ShowDebug("Download skipped: URL " + p.Right.ImagePath + ": file already exists.");
						p.Right.ImagePath = p.Left;
					}
					else
					{
						Program.mainForm.Invoke(upc, 0);
						Program.mainForm.Invoke(new System.Windows.Forms.MethodInvoker(Program.mainForm.ShowProgress));

						Stream sResp = null, sLocal = null;
						DateTime start = DateTime.Now;
						try
						{
							HttpWebRequest req = (HttpWebRequest) WebRequest.Create(p.Right.ImagePath);
							req.Credentials = CredentialCache.DefaultCredentials;
							req.Method = "HEAD";
							HttpWebResponse resp = (HttpWebResponse) req.GetResponse();
							long fileSize = resp.ContentLength;
							resp.Close();

							Program.mainForm.Invoke(ust, "Downloading file: 0/" + fileSize + " bytes downloaded");
							DebugConsole.ShowDebug("Download start: URL " + p.Right.ImagePath + " to " + p.Left);

							using (sResp = wc.OpenRead(p.Right.ImagePath))
							{
								using (sLocal = new FileStream(p.Left + ".tmp", FileMode.Create, FileAccess.Write, FileShare.None))
								{
									int bytesSize = 0;
									byte[] buffer = new byte[1024];

									while ((bytesSize = sResp.Read(buffer, 0, buffer.Length)) > 0)
									{
										sLocal.Write(buffer, 0, bytesSize);
										this.bytesDownloaded += bytesSize;

										Program.mainForm.Invoke(upc, (int) ((double) sLocal.Length / fileSize * 100));
										Program.mainForm.Invoke(ust, "Downloading file: " + sLocal.Length + "/" + fileSize + " bytes downloaded");
									}
								}
							}

							File.Move(p.Left + ".tmp", p.Left);
							p.Right.ImagePath = p.Left;
						}
						catch (Exception e)
						{
							DebugConsole.ShowWarning("Download of URL " + p.Right.ImagePath + " failed (" + e.Message + ")");
						}
						finally
						{
							Program.mainForm.Invoke(ust, "Download complete.");
							Program.mainForm.Invoke(new System.Windows.Forms.MethodInvoker(Program.mainForm.HideProgress));

							if (File.Exists(p.Left))
								DebugConsole.ShowInfo("Downloaded " + Program._humanReadableFileSize(new FileInfo(p.Left).Length) + " from " + p.Right.ImagePath + " in " + Math.Round((DateTime.Now - start).TotalMilliseconds / 1000.0, 2) + " seconds.");
						}
					}
					this.jobsFinished++;
				}
			}
			catch { }
		}

		private Thread _createWorker()
		{
			Thread t = new Thread(this._worker);
			t.Name = "Worker Thread";
			t.Start();

			return t;
		}
		private void _worker()
		{
			try {
			while (Thread.CurrentThread.ThreadState == ThreadState.Running)
			{
				mre.WaitOne(); // Wait until we are signalled.

				Pair<WaitCallback, object> job = null;
				lock (this.work) // Get a lock on the queue, only one thread at a time executes this code.
				{
					if (this.work.Count == 0) // We were beat to the job; continue.
						continue;

					job = this.work.Dequeue();

					if (this.work.Count == 0) // No more work left, reset the signal.
						mre.Reset();
				}

				if (job == null) // Safety.
					continue;

				try {
					job.Left.Invoke(job.Right);
				} catch (Exception e) {
					DebugConsole.ShowWarning("Exception thrown while processing download job: " + e.GetType().Name + " " + e.Message);
				}
			} 
			} catch(ThreadAbortException) { }
		}

		public class Pair<T1, T2>
		{
			public T1 Left;
			public T2 Right;

			public Pair(T1 left, T2 right)
			{
				this.Left = left;
				this.Right = right;
			}
		}
	}
}
