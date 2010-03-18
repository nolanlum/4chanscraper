using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Scraper.Data
{
	[Serializable]
	public class ThreadDatabase : IDisposable//, IEnumerable<KeyValuePair<int, Thread>>
	{
		public delegate void __ThreadAdd(Thread newThread);
		public event __ThreadAdd ThreadAdded;

		#region Private Members
		[NonSerialized]
		private static readonly long VERSION = -1L;

		private string name, filename, url;
		private bool crawledAll;
		private Dictionary<int, Thread> threads;

		[NonSerialized]
		private FileStream fileHandle;
		[NonSerialized]
		private bool drawnTreeOnce;
		[NonSerialized]
		private string imagedir;
		#endregion

		#region Public Properties
		public string URL
		{
			get { return this.url; }
			set { this.url = value; }
		}
		public string Filename
		{
			get { return this.filename; }
		}
		public string ImageDir
		{
			get { if (this.imagedir != null) return this.imagedir; FileInfo fi = new FileInfo(this.filename); return this.imagedir = fi.DirectoryName + @"\" + fi.Name.Replace(fi.Extension, ""); }
		}
		public int ThreadCount
		{
			get { return this.threads.Count; }
		}
		public int PostCount
		{
			get { int c = 0; foreach (Thread t in this.threads.Values) c += t.Count; return c; }
		}
		public long SerializedSize
		{
			get { return this.fileHandle.Length; }
		}
		public bool CrawledAllPages
		{
			get { return this.crawledAll; }
			set { this.crawledAll = value; }
		}
		public bool FirstDraw
		{
			get { return this.drawnTreeOnce; }
			set { this.drawnTreeOnce = value; }
		}
		public Thread this[int id]
		{
			get
			{
				if (!threads.ContainsKey(id))
					return null;
				else
					return threads[id];
			}
			set
			{
				this.threads[id] = value;
			}
		}
		public Thread this[string name]
		{
			get
			{
				foreach (Thread t in this.threads.Values)
					if (t.Name == name) return t;
				return null;
			}
			set
			{
				this.threads[value.Id] = value;
			}
		}
		#endregion

		#region Constructor & Dispose
		public ThreadDatabase(string name, string filename, string url)
		{
			this.name = name;
			this.filename = filename;
			this.url = url;

			this.fileHandle = new FileStream(this.filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			this.threads = new Dictionary<int, Thread>();
		}
		public void Dispose()
		{
			if (this.fileHandle != null)
			{
				this.fileHandle.Flush();
				this.fileHandle.Close();
				this.fileHandle.Dispose();
			}
		}
		#endregion

		public void AddThread(Thread thread)
		{
			if (this.threads.ContainsKey(thread.Id))
				if (this.threads[thread.Id].Count == thread.Count)
					return;
				else
				{
					thread = this.threads[thread.Id] + thread;
					this.threads.Remove(thread.Id);
				}

			thread.IsNewThread = thread.HasNewPosts;
			this.threads.Add(thread.Id, thread);

			if (this.ThreadAdded != null)
				this.ThreadAdded(thread);
		}
		public void AddThreads(IEnumerable<Thread> threads)
		{
			foreach (Thread t in threads)
				AddThread(t);
		}

		public void RemoveThread(Thread thread)
		{
			this.threads.Remove(thread.Id);
		}

		public Post FindPost(int id)
		{
			foreach (Thread t in this.threads.Values)
				for (int i = 0; i < t.Count; i++)
					if (t[i].Id == id)
						return t[i];

			return null;
		}

		public void Save()
		{
			DebugConsole.ShowInfo("Saving database to file: " + filename);
			try
			{
				IFormatter formatter = new BinaryFormatter();

				this.fileHandle.SetLength(0);
				this.fileHandle.Seek(0, SeekOrigin.Begin);
				formatter.Serialize(this.fileHandle, ThreadDatabase.VERSION);
				formatter.Serialize(this.fileHandle, this);
			}
			catch (Exception e)
			{
				DebugConsole.ShowError("Exception thrown while saving database: " + e.ToString());
			}
			finally
			{
				this.fileHandle.Flush();
			}
			DebugConsole.ShowInfo("Serialized " + this.ThreadCount + " threads.");
		}

		#region Enumerator Methods
		public IEnumerator<KeyValuePair<int, Thread>> GetEnumerator()
		{
			return this.threads.GetEnumerator();
		}
		#endregion

		#region Static Methods
		public static ThreadDatabase LoadFromFile(string filename)
		{
			Stream stream = null;
			ThreadDatabase db = null;
			DebugConsole.ShowInfo("Loading database from file: " + filename);
			try
			{
				IFormatter formatter = new BinaryFormatter();
				stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				long version = (long) formatter.Deserialize(stream);
				Debug.Assert(version == VERSION, "Unable to load database, incompatible file version.", "The specified database was created with an incompatible version of this program.");

				db = (ThreadDatabase) formatter.Deserialize(stream);
			}
			catch (Exception e)
			{
				DebugConsole.ShowError("Exception thrown while loading database: " + e.ToString());
			}
			finally
			{
				if (null != stream)
					stream.Close();
				if (null != db)
				{
					db.filename = new FileInfo(filename).FullName;
					db.fileHandle = new FileStream(db.filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

					// Convert path to absolute.
					foreach (KeyValuePair<int, Thread> kvp in db.threads)
						foreach (Post p in kvp.Value)
							if (!p.ImagePath.Contains("http:"))
								p.ImagePath = db.ImageDir + @"\" + p.ImagePath;
				}
			}

			return db;
		}
		#endregion
	}
}
