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
	class ThreadDatabase : IDisposable, IEnumerable<KeyValuePair<int, Thread>>
	{
		public delegate void __ThreadAdd(Thread newThread);
		public event __ThreadAdd ThreadAdded;

		#region Private Members
		[NonSerialized]
		private static readonly long VERSION = -1L;

		private string name, filename, url;
		private Dictionary<int, Thread> threads;

		[NonSerialized]
		private FileStream fileHandle;
		#endregion

		#region Public Properties
		public string URL
		{
			get { return this.url; }
			set { this.url = value; }
		}
		public int ThreadCount
		{
			get { return this.threads.Count; }
		}
		public int PostCount
		{
			get { int c = 0; foreach (Thread t in this.threads.Values) c += t.Count; return c; }
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
			this.threads.Add(thread.Id, thread);

			if (this.ThreadAdded != null)
				this.ThreadAdded(thread);
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
		IEnumerator IEnumerable.GetEnumerator()
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
			}

			return db;
		}
		#endregion
	}
}
