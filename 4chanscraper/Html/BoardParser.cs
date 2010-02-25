﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scraper.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Scraper.Html
{
	class BoardParser
	{
		#region Private Members
		private string url;
		private List<Thread> threadList;
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
			this.url = url;
			this.threadList = new List<Thread>();
		}

		public void Parse()
		{
			string page;
			Regex r = new Regex("<span id=\"nothread([0-9]*)\">", RegexOptions.IgnoreCase);

			// Get page as a string, replacing all newlines.
			try
			{
				using (WebClient client = new WebClient())
				{
					//using (StreamReader reader = new StreamReader(client.OpenRead(url)))
					using (StreamReader reader = new StreamReader("imgboard.html.txt"))
					{
						page = reader.ReadToEnd().Replace("\n", "").Replace("\r", "");
					}
				}
			}
			catch (Exception e)
			{
				page = null;
				DebugConsole.ShowError("Error parsing board page: " + e.GetType().Name + " " + e.Message);
			}

			if (page == null)
				return;

			// Extract thread information.
			string[] parts = page.Split(new string[] { "<hr>" }, StringSplitOptions.RemoveEmptyEntries);
			List<Thread> posts = new List<Thread>();
			for (int i = 0; i < parts.Length; i++)
			{
				if (parts[i].StartsWith("<span class=\"filesize\">") || parts[i].StartsWith("<form"))
				{
					Match m = r.Match(parts[i]);
					if (m.Success)
						posts.Add(new Thread(int.Parse(m.Groups[1].Value)));
				}
			}

			// Now crawl each individual thread for images.
		}

		private void CrawlThread(Thread t)
		{

		}
	}
}