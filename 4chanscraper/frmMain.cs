using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Scraper.Data;
using Scraper.Html;
using SysThread = System.Threading.Thread;
using ThreadStart = System.Threading.ThreadStart;

namespace Scraper
{
	public partial class frmMain : Form
	{
		public delegate void __UpdateStatusText(string newtext);
		
		private static Regex imgnameR = new Regex("[0-9]+\\.[a-z]{3,4}", RegexOptions.IgnoreCase);

		private bool _enableAutoScrape;
		private int _downloaderThreads;

		private SysThread _threadParse;
		private ImageDownloader _downloader;

		public bool EnableAutoScrape
		{
			get { return this._enableAutoScrape; }
			set
			{
				this.cmTaskTray_Enabled.Checked = value;
				this.mnuMain_ScraperEnabled.Checked = value;
				this._enableAutoScrape = value;
			}
		}
		public int DownloaderThreads
		{
			get { return this._downloaderThreads; }
			set
			{
				if (!(value >= 1 && value <= 4)) return;
				this.mnuMain_ScraperThreads.SelectedIndex = value - 1;

				this._downloaderThreads = value;
				this._downloader.DownloadThreads = value;
			}
		}

		public frmMain()
		{
			InitializeComponent();
#if DEBUG
			this.mnuMain_HelpDebug.Checked = true;
#endif
			this.mnuMain_ScraperThreads.SelectedIndex = 0;
			this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);

			this._downloader = new ImageDownloader(1);
		}
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Put stuff to interrupt downloads, save databases, etc.
			DebugConsole.ShowStatus("Main form received close request; terminating all background tasks.");

			if (this._threadParse != null && this._threadParse.IsAlive)
				try { this._threadParse.Abort(); }
				catch { }

			if (this._downloader != null)
				this._downloader.Dispose();
		}

		public void DrawDatabaseTree(ThreadDatabase db)
		{
			try
			{
				this.treePostWindow.SuspendLayout();
				this.treePostWindow.Nodes.Clear();

				TreeNode[] tree = new TreeNode[db.ThreadCount]; int j = 0;
				foreach (KeyValuePair<int, Thread> kvp in db)
				{
					TreeNode[] children = new TreeNode[kvp.Value.Count];
					for (int i = 0; i < kvp.Value.Count; i++)
					{
						children[i] = new TreeNode(kvp.Value[i].Id + (i == 0 ? " (OP)" : ""));
						children[i].Tag = "post";
					}
					tree[j] = new TreeNode(kvp.Value.Id.ToString(), children);
					tree[j++].Tag = "thread";
				}

				this.treePostWindow.Nodes.AddRange(tree);
				this.treePostWindow.ResumeLayout(false);
				this.treePostWindow.PerformLayout();
			}
			catch (InvalidOperationException) { }
		}

		public void UpdateStatusText(string newText)
		{
			try
			{
				this.lblStatus.Text = newText;
			}
			catch (InvalidOperationException) { }
		}

		private void _crawlDb(ThreadDatabase db)
		{
			try
			{
				FileInfo fi = new FileInfo(db.Filename);
				string foldername = fi.Name.Replace(fi.Extension, "");
				if (!Directory.Exists(foldername))
					Directory.CreateDirectory(foldername);

				foreach (KeyValuePair<int, Thread> kvp in db)
				{
					Thread t = kvp.Value;
					for (int i = 0; i < t.Count; i++)
						if (t[i].ImagePath.Contains("http"))
							this._downloader.QueuePost(foldername + "\\" + imgnameR.Match(t[i].ImagePath).Value, t[i]);
				}

			}
			catch (IOException ioe)
			{
				DebugConsole.ShowError("IO error occurred while downloading images: " + ioe.GetType().Name + " " + ioe.Message);
			}
			catch (UnauthorizedAccessException)
			{
				DebugConsole.ShowError("File permissions on image directory too restrictive, cannot write to directory. Aborting download.");
			}
		}

		#region Event Listeners
		#region Menu Items
		#region File
		private void mnuMain_FileMinimize_Click(object sender, EventArgs e)
		{
			this.mnuMain_FileMinimize.Checked = !this.mnuMain_FileMinimize.Checked;
		}
		private void mnuMain_FileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion
		#region Scraper Config
		private void mnuMain_ScraperConf_Click(object sender, EventArgs e)
		{
			Dialogs.frmInputDialog input = new Scraper.Dialogs.frmInputDialog("Enter the amount of time between automatic scrapes, with a time unit following the number. (h=hour,m=minute,s=second)\nYou may choose any combination of the units.\nEx: 30s = 30 seconds; 5m30s = 5 minutes, 30 seconds");
			TimeSpan sp = new TimeSpan(this.timerAutoScrape.Interval * 10000);
			input.InputText = string.Format("{0}h{1}m{2}s", sp.Hours, sp.Minutes, sp.Seconds).Replace("0h", "").Replace("0m","").Replace("0s","");
			input.ShowDialog();

			string timestring = input.InputText.Trim();
			if (timestring == "")
				return;

			Regex h = new Regex("([0-9]*)h", RegexOptions.IgnoreCase), m = new Regex("([0-9]*)m", RegexOptions.IgnoreCase), s = new Regex("([0-9]*)s", RegexOptions.IgnoreCase);
			Match hm = h.Match(timestring), mm = m.Match(timestring), sm = s.Match(timestring);
			int newInterval = 0;

			if(hm.Success)
				newInterval += int.Parse(hm.Groups[1].Value) * 3600000;
			if(mm.Success)
				newInterval += int.Parse(mm.Groups[1].Value) * 60000;
			if (sm.Success)
				newInterval += int.Parse(sm.Groups[1].Value) * 1000;

			if (newInterval == 0)
				MessageBox.Show("There was an error parsing your input; ensure you specified a valid time string.", "4chanscraper", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			else
				this.timerAutoScrape.Interval = newInterval;
		}
		private void mnuMain_ScraperThreads_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._downloaderThreads = this.mnuMain_ScraperThreads.SelectedIndex - 1;
		}
		private void mnuMain_ScraperNow_Click(object sender, EventArgs e)
		{
			ThreadDatabase db = new ThreadDatabase("/w/", "w.dat", "http://boards.4chan.org/w/");
			this._threadParse = new SysThread(new ThreadStart(delegate() { db.AddThreads(new BoardParser(db.URL).Parse()); }));
			this._threadParse.Start();
			while (this._threadParse.IsAlive)
				Application.DoEvents();
			this.DrawDatabaseTree(db);
			this._crawlDb(db);
		}
		#endregion
		#region Help
		private void mnuMain_HelpAbout_Click(object sender, EventArgs e)
		{
			new frmAbout().ShowDialog();
		}
		private void mnuMain_HelpDebug_Click(object sender, EventArgs e)
		{
			if (this.mnuMain_HelpDebug.Checked)
				DebugConsole.HideConsole();
			else
				DebugConsole.ShowConsole();

			this.mnuMain_HelpDebug.Checked = !this.mnuMain_HelpDebug.Checked;
		}
		#endregion
		#endregion

		#region Taskbar Items
		private void cmTaskTray_Show_Click(object sender, EventArgs e)
		{
			this.taskTrayIcon_DoubleClick(sender, e);
		}
		
		private void cmTaskTray_Enabled_Click(object sender, EventArgs e)
		{
			this.EnableAutoScrape = !this.EnableAutoScrape;
		}

		private void cmTaskTray_Close_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		void frmMain_Resize(object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized && this.mnuMain_FileMinimize.Checked)
				this.Hide();
		}
		void taskTrayIcon_DoubleClick(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;

			this.TopMost = true;
			this.Focus();
			this.BringToFront();
			this.TopMost = false;
		}
		#endregion

		
	}
}
