using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Scraper.Html
{
	public class BoardListParser
	{
		private static Regex r = new Regex("<a href=\"(?<url>[^\"]*)\" title=\"[^\"]*\">.{1,4}</a>", RegexOptions.IgnoreCase);
		
		private string url, page;
		private string[] urls;

		public BoardListParser(string url)
		{
			this.url = url;
		}

		public string[] GetBoardURLs()
		{
			if(this.urls != null)
				return this.urls;

			if (!getPage())
				return null;

			List<string> l = new List<string>();
			MatchCollection mc = r.Matches(this.page);
			foreach (Match m in mc)
			{
				if (!l.Contains(m.Groups["url"].Value) && m.Groups["url"].Value.Contains("boards"))
					l.Add(m.Groups["url"].Value);
			}

			DebugConsole.ShowInfo("Found " + l.Count + " unique boards.");
			this.urls = new string[l.Count];
			l.CopyTo(this.urls);
			return this.urls;
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
	}
}
