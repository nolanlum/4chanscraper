using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scraper.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Scraper.Html
{
	public class BoardParser : IDisposable
	{
		#region Private Members
		private string url, page;
		private List<Thread> threadList;
		private bool disposing = false;

		private static Regex pageNum = new Regex("\\[<a href=\"([0-9]*)\">[0-9]*</a>\\]", RegexOptions.IgnoreCase);
		private static Regex threadID = new Regex("<span id=\"nothread([0-9]*)\">", RegexOptions.IgnoreCase);
		private static Regex postOP = new Regex("File .*<a href=\"(?<url>.*)\" t.*-\\((?<size>[0-9. KMB]*), (?<res>[0-9]*x[0-9]*).*\\).*</span> (?<date>.*)<span id=\"nothread(?<id>[0-9]*).*<blockquote>(?<text>.*)</blockquote>");
		private static Regex postRE = new Regex("<span class=\"commentpostername\".*</span> (?<date>.*) <span id=\"norep(?<id>[0-9]*).*File.*<a href=\"(?<url>.*)\" t.*-\\(([?<size>0-9. KMB]*), (?<res>[0-9]*x[0-9]*).*\\).*<blockquote>(?<text>.*)</blockquote>");
		#endregion

		#region Public Properties
		public string URL
		{
			get { return this.url; }
		}
		public Thread[] Threads
		{
			get
			{
				if (this.threadList.Count == 0)
					return null;
				else
				{
					Thread[] a = new Thread[this.threadList.Count];
					this.threadList.CopyTo(a);
					return a;
				}
			}
		}
		#endregion

		public BoardParser(string url)
		{
			this.url = url.Replace('\\', '/').Replace("/imgboard.html", "");
			this.threadList = new List<Thread>();
		}
		public void Dispose()
		{
			if (this.disposing) return;

			this.page = this.url = null;
			this.threadList.Clear();
			this.threadList = null;
		}

		public int DetectPageCount()
		{
			if (this.disposing) throw new ObjectDisposedException("BoardParser");
			if (!getPage())
				return 10;

			MatchCollection mc = pageNum.Matches(this.page);
			int max = 0;
			foreach (Match m in mc)
				max = Math.Max(max, int.Parse(m.Groups[1].Value));

			DebugConsole.ShowInfo("Autodetected current board has " + max + " pages.");
			return max;
		}

		public Thread[] Parse()
		{
			if (this.disposing) throw new ObjectDisposedException("BoardParser");
			if (!getPage())
				return null;

			// Extract thread information.
			DebugConsole.ShowInfo("Parsing board page:", " ");
			string[] parts = this.page.Split(new string[] { "<hr>" }, StringSplitOptions.RemoveEmptyEntries);
			List<Thread> threads = new List<Thread>();
			for (int i = 0; i < parts.Length; i++)
			{
				if (parts[i].StartsWith("<span class=\"filesize\">") || parts[i].StartsWith("<form"))
				{
					Match m = threadID.Match(parts[i]);
					if (m.Success)
						threads.Add(new Thread(int.Parse(m.Groups[1].Value)));
				}
			}
			DebugConsole.WriteParseANSI("found " + threads.Count + " threads.\n");

			// Now crawl each individual thread for images.
			foreach (Thread t in threads)
				//System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(this.crawlThread), t);
				this.CrawlThread(t);

			Thread[] ta = new Thread[threads.Count];
			threads.CopyTo(ta);
			return ta;
		}

		public void CrawlThread(object t)
		{
			if (t.GetType() == typeof(Thread)) CrawlThread((Thread) t);
		}
		public void CrawlThread(Thread t)
		{
			if (this.disposing) throw new ObjectDisposedException("BoardParser");
			string page = null, url = this.url.TrimEnd("1234567890".ToCharArray()) + "res/" + t.Id;
			DebugConsole.ShowDebug("Retrieving thread page: " + url);
			try
			{
				using (WebClient client = new WebClient())
				{
					using (StreamReader reader = new StreamReader(client.OpenRead(url)))
					//using (StreamReader reader = new StreamReader("..\\..\\Examples\\957853.html.txt"))
					{
						page = reader.ReadToEnd().Replace("\n", "").Replace("\r", "");
					}
				}
			}
			catch (Exception e)
			{
				page = null;
				DebugConsole.ShowError("Error retrieving thread page: " + e.GetType().Name + " " + e.Message);
			}

			if (page == null)
				return;

			// Extract post information.
			DebugConsole.ShowInfo("Parsing thread No." + t.Id + ":", " ");
			string[] parts = page.Split(new string[] { "<hr>" }, StringSplitOptions.RemoveEmptyEntries);//
			for (int i = 0; i < parts.Length; i++)
			{
				if (parts[i].StartsWith("<form"))
				{
					parts = parts[i].Split(new string[] { "<td nowrap class=\"doubledash\">" }, StringSplitOptions.RemoveEmptyEntries);
					break;
				}
			}
			for (int i = 0; i < parts.Length; i++)
			{
				Match m = (i == 0 ? postOP : postRE).Match(parts[i]);
				if (m.Success)
				{
					t.AddPost(new Post(int.Parse(m.Groups["id"].Value), m.Groups["text"].Value, m.Groups["url"].Value, this.parse4chanDate(m.Groups["date"].Value.Trim())));
				}
			}
			DebugConsole.WriteParseANSI("found " + t.Count + " images/posts.\n");

			
		}

		private bool getPage()
		{
			if (this.page != null)
				return true;

			DebugConsole.ShowDebug("Retrieving board page: " + this.url);

			// Get page as a string, replacing all newlines.
			try
			{
				using (WebClient client = new WebClient())
				{
					using (StreamReader reader = new StreamReader(client.OpenRead(this.url)))
					//using (StreamReader reader = new StreamReader("..\\..\\Examples\\imgboard.html.txt"))
					{
						this.page = reader.ReadToEnd().Replace("\n", "").Replace("\r", "");
					}
				}
			}
			catch (Exception e)
			{
				this.page = null;
				DebugConsole.ShowError("Error retrieving board page: " + e.GetType().Name + " " + e.Message);
			}

			return true;
		}

		private DateTime parse4chanDate(string date)
		{
			return DateTime.ParseExact(date, "MM/dd/yy(ddd)HH:mm", CultureInfo.InvariantCulture);
		}
	}
}
