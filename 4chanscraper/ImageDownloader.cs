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

		public ImageDownloader(int size)
		{
			this.work = new Queue<Pair<WaitCallback, object>>();

			this.workers = new Thread[size];
			for (int i = 0; i < size; i++){
				this.workers[i] = _createWorker();
			}
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
			}

			this.mre.Set();
		}

		public void Dispose()
		{
			if (this.disposing)
				return;
			else
				this.disposing = true;

			for (int i = 0; i < this.workers.Length; i++)
				this.workers[i].Abort();

			this.workers = null;
			this.work = null;
		}

		private void _downloadPostImage(object o)
		{
			if (o.GetType() != typeof(Pair<string, Data.Post>))
			{
				DebugConsole.ShowWarning("Download thread passed object that was not a post; bug?");
				return;
			}

			Pair<string, Data.Post> p = (Pair<string, Data.Post>) o;
			using (WebClient wc = new WebClient())
			{
				if (File.Exists(p.Left))
				{ // Assume already downloaded.
					DebugConsole.ShowDebug("Download skipped: URL " + p.Right.ImagePath + ": file already exists.");
				}
				else
				{
					DebugConsole.ShowDebug("Download start: URL " + p.Right.ImagePath + " to " + p.Left);


					DateTime start = DateTime.Now;
					wc.DownloadFile(p.Right.ImagePath, p.Left);
					if (File.Exists(p.Left))
						DebugConsole.ShowInfo("Downloaded " + _humanReadableFileSize(new FileInfo(p.Left).Length) + " from " + p.Right.ImagePath + " in " + Math.Round((DateTime.Now - start).TotalMilliseconds / 1000.0, 2) + " seconds.");
					else
						DebugConsole.ShowWarning("Download of URL " + p.Right.ImagePath + " failed.");
				}
			}
			p.Right.ImagePath = Path.GetFullPath(p.Left);
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

		private string _humanReadableFileSize(long size)
		{
			if (size > 1048576L)
				return Math.Round(size / 1048576.0, 2) + " MB";
			else if (size > 1024L)
				return Math.Round(size / 1024.0, 2) + " KB";
			else
				return size + " bytes";
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
