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
		public delegate void __UpdateProgessCallback(int percentage);

		private static Regex imgnameR = new Regex(@"[0-9]+\.[a-z]{3,4}", RegexOptions.IgnoreCase);
		private static Regex boardnameR = new Regex(@"http://.*?/([0-9a-z]){1,4}/", RegexOptions.IgnoreCase);
		private static Regex threadIdR = new Regex(@"http://.*?/res/([0-9]*)", RegexOptions.IgnoreCase);

		private bool _running = true, _updatingStatus = false;
		private bool _enableAutoScrape = false, _enableScrapeImages = true;
		private int _downloaderThreads = 1;

		private SysThread _threadParse;
		private ImageDownloader _downloader;
		private GenericCache<string, Image> _imageCache;

		private ThreadDatabase _db;

		private ContextMenu cmTree;
		private MenuItem cmTree_Rename, cmTree_Delete, cmTree_Rescrape, cmTree_Download, cmTree_OpenInExplorer, cmTree_GenerateImgUrl, cmTree_Sep1, cmTree_Sep2;
		private MenuItem[] cmTree__Thread, cmTree__Post;
		private TreeNode treePostWindowMouseAt;
		private string _tempNodeText;

		public bool EnableAutoScrape
		{
			get { return this._enableAutoScrape; }
			set
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					this.cmTaskTray_Enabled.Checked = value;
					this.mnuMain_ScraperEnabled.Checked = value;
				}));
				this._enableAutoScrape = value;
			}
		}
		public bool EnableScrapeImages
		{
			get { return this._enableScrapeImages; }
			set
			{
				this.Invoke(new MethodInvoker(delegate()
				{
					this.mnuMain_ScraperMode_Metadata.Checked = !value;
					this.mnuMain_ScraperMode_MetadataAndImages.Checked = value;
				}));
				this._enableScrapeImages = value;
			}
		}
		public bool EnableScrapeAll
		{
			get { if (this._db == null) return false; else return !this._db.CrawledAllPages; }
			set { if (this._db == null) return; this._db.CrawledAllPages = !value; this.Invoke(new MethodInvoker(delegate() { this.mnuMain_ScraperAll.Checked = value; })); }
		}
		public int DownloaderThreads
		{
			get { return this._downloaderThreads; }
			set
			{
				if (!(value >= 1 && value <= 4)) return;
				this.Invoke(new MethodInvoker(delegate() { this.mnuMain_ScraperThreads.SelectedIndex = value - 1; }));

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
			this._imageCache = new GenericCache<string, Image>(5);

			// Windows XP visuals fix.
			if (Environment.OSVersion.Version.Major == 5)
			{
				this.grpStatus.Location = new Point(247, 27);
				this.grpStatus.Size -= new Size(0, 6);
				this.grpPostStats.Location = new Point(0, 76);
				this.grpPostStats.Size -= new Size(0, 13);
			}

			this.treePostWindow.TreeViewNodeSorter = new TreeViewComparer();
			#region Tree View Context Menu Setup
			this.cmTree = new ContextMenu();
			this.cmTree.Name = "cmTree";
			this.cmTree.Popup += new EventHandler(this.cmTree_Popup);
			this.cmTree_Rename = new MenuItem("Rename Thread");
			this.cmTree_Rename.Name = "cmTree_Rename";
			this.cmTree_Rename.Click += new EventHandler(cmTree_Rename_Click);
			this.cmTree_Delete = new MenuItem("Delete");
			this.cmTree_Delete.Name = "cmTree_Delete";
			this.cmTree_Delete.Shortcut = Shortcut.Del;
			this.cmTree_Delete.Click += new EventHandler(cmTree_Delete_Click);
			this.cmTree_Rescrape = new MenuItem("Rescrape Thread");
			this.cmTree_Rescrape.Name = "cmTree_Rescrape";
			this.cmTree_Rescrape.Shortcut = Shortcut.CtrlR;
			this.cmTree_Rescrape.Click += new EventHandler(cmTree_Rescrape_Click);
			this.cmTree_Download = new MenuItem("Download");
			this.cmTree_Download.Name = "cmTree_Download";
			this.cmTree_Download.Shortcut = Shortcut.CtrlD;
			this.cmTree_Download.Click += new EventHandler(cmTree_Download_Click);
			this.cmTree_OpenInExplorer = new MenuItem("Show Image in Explorer");
			this.cmTree_OpenInExplorer.Name = "cmTree_OpenInExplorer";
			this.cmTree_OpenInExplorer.Click += new EventHandler(cmTree_OpenInExplorer_Click);
			this.cmTree_GenerateImgUrl = new MenuItem("Copy 4chan Image URL");
			this.cmTree_GenerateImgUrl.Name = "cmTree_GenerateImgUrl";
			this.cmTree_GenerateImgUrl.Click += new EventHandler(cmTree_GenerateImgUrl_Click);
			this.cmTree_Sep1 = new MenuItem("-");
			this.cmTree_Sep1.Name = "cmTree_Sep1";
			this.cmTree_Sep2 = new MenuItem("-");
			this.cmTree_Sep2.Name = "cmTree_Sep2";
			this.cmTree__Thread = new MenuItem[] { this.cmTree_Rescrape, this.cmTree_Download, this.cmTree_Sep1, this.cmTree_Rename, this.cmTree_Delete };
			this.cmTree__Post = new MenuItem[] { this.cmTree_Download, this.cmTree_Sep1, this.cmTree_Delete, this.cmTree_Sep2, this.cmTree_OpenInExplorer, this.cmTree_GenerateImgUrl };
			this.cmTree.MenuItems.AddRange(this.cmTree__Thread);
			this.treePostWindow.ContextMenu = this.cmTree;
			#endregion
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Put stuff to interrupt downloads, save databases, etc.
			DebugConsole.ShowStatus("Main form received close request; terminating all background tasks.");
			this._running = false;

			if (this._threadParse != null && this._threadParse.IsAlive)
				try { this._threadParse.Abort(); }
				catch { }

			if (this._downloader != null)
				this._downloader.Dispose();

			if (this._db != null)
				try { this._db.Save(); this._db.Dispose(); }
				catch { }
		}

		public void DrawDatabaseTree(ThreadDatabase db)
		{
			this.UpdateStatusText("Redrawing database tree...");

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
						if (kvp.Value[i].IsNewPost)
						{
							children[i].ForeColor = Color.LimeGreen;
							kvp.Value[i].IsNewPost = false;
						}

						children[i].Tag = "post";
					}

					tree[j] = new TreeNode(kvp.Value.Name != null ? kvp.Value.Name : kvp.Value.Id.ToString(), children);
					if (kvp.Value.IsNewThread)
					{
						tree[j].ForeColor = Color.LimeGreen;
						kvp.Value.IsNewThread = false;
					}

					tree[j++].Tag = "thread";
				}

				this.treePostWindow.Nodes.AddRange(tree);
				this.treePostWindow.Sort();
				this.treePostWindow.ResumeLayout(false);
				this.treePostWindow.PerformLayout();
			}
			catch (InvalidOperationException) { }

			this.UpdateStatusText("Ready.");
			Application.DoEvents();
		}

		public void LoadDatabase(string filename)
		{
			this.UpdateStatusText("Loading database...");

			if (this._db != null)
				this._db.Dispose();

			this._db = ThreadDatabase.LoadFromFile(filename);
			if (this._db == null)
				Program._genericMessageBox("An error occurred loading the database. Ensure you have specified a valid database. Check the Debug Console for more information.", MessageBoxIcon.Error);
			else
			{
				DrawDatabaseTree(this._db);

				this.grpPostStats.Hide();
				this.pnlDetails.Show();
				this.mnuMain_Scraper.Enabled = true;
			}

			this.UpdateStatusText("Ready.");
		}
		public void UpdatePostDetails(Post p)
		{
			if (p == null) { this.grpPostStats.Hide(); return; }
			
			this.grpPostStats.Show();

			this.lblPostDate.Text = p.PostTime.ToString("MM/dd/yy(ddd)HH:mm");
			this.lblPostImgPath.Text = p.ImagePath;
			frmMain_ToolTip.SetToolTip(this.lblPostImgPath, p.ImagePath);
			if (p.ImagePath.Contains("http:"))
			{
				this.lblPostImgInfo.Text = "Unavailable.";
				this.picPostImg.Visible = false;
			}
			else
			{
				// Blank the picturebox.
				if (this.picPostImg.Image != null)
					this.picPostImg.Image.Dispose();
				this.picPostImg.Image = new Bitmap(Math.Max(1, this.picPostImg.Width), Math.Max(1, this.picPostImg.Height));

				// Blt the image.
				Image i = this._getImage(p.ImagePath);
				this.lblPostImgInfo.Text = string.Format("{0} ({1}x{2})", Program._humanReadableFileSize(new FileInfo(p.ImagePath).Length), i.Width, i.Height);
				this._resizeImage(this.picPostImg.Image, i);
				this.picPostImg.Visible = true;
			}
			this.picPostImg.Tag = p;
		}
		public void UpdatePostDetails()
		{
			if (this.picPostImg.Tag != null && this.picPostImg.Tag.GetType() == typeof(Post)) this.UpdatePostDetails(this.picPostImg.Tag as Post);
		}

		#region Crawling logic and helpers
		public void ScrapeBoard()
		{
			if (this._threadParse != null && this._threadParse.IsAlive)
			{
				Program._genericMessageBox("A metadata scrape is already in progress. Please wait until the current metadata scrape is complete.", MessageBoxIcon.Warning); return;
			}

			List<Thread> newThreads = new List<Thread>();
			this._threadParse = new SysThread(new ThreadStart(delegate()
			{
				BoardParser bp = new BoardParser(this._db.URL);

				if (this.EnableScrapeAll)
				{
					int pages = bp.DetectPageCount();
					string[] urls = new string[pages];
					this.Invoke(new __UpdateStatusText(this.UpdateStatusText), "Grabbing metadata for " + pages + " pages...this may take a while.");

					for (int i = 1; i <= pages; i++)
					{
						urls[pages - i] = this._db.URL.TrimEnd('/') + "/" + (i == 1 ? "" : i.ToString());
					}
					foreach (string s in urls)
						newThreads.AddRange(new BoardParser(s).Parse());

					this.EnableScrapeAll = false;
				}
				else
				{
					this.Invoke(new __UpdateStatusText(this.UpdateStatusText), "Grabbing metadata...this may take a while.");
					newThreads.AddRange(new BoardParser(this._db.URL).Parse());
				}
			}));
			this._threadParse.Start();

			while (this._threadParse.IsAlive)
			{
				Application.DoEvents();
				SysThread.Sleep(50);
			}

			this._db.AddThreads(newThreads);
			this.DrawDatabaseTree(this._db);

			if (!Directory.Exists(this._db.ImageDir))
				Directory.CreateDirectory(this._db.ImageDir);
			this._crawlThreads(newThreads, this._db.ImageDir);
			this._statusLoopDownloading();
		}

		private void _crawlThreads(IEnumerable<Thread> tt, string foldername, bool force)
		{
			foreach (Thread t in tt)
				this._crawlThread(t, foldername, force);
		}
		private void _crawlThreads(IEnumerable<Thread> tt, string foldername)
		{
			this._crawlThreads(tt, foldername, false);
		}
		private void _crawlThread(Thread t, string foldername, bool force)
		{
			if (!(this._enableScrapeImages || force)) return;

			for (int i = 0; i < t.Count; i++)
				if (t[i].ImagePath.Contains("http:"))
					this._downloader.QueuePost(foldername + @"\" + imgnameR.Match(t[i].ImagePath).Value, t[i]);
		}
		private void _crawlThread(Thread t, string foldername) { _crawlThread(t, foldername, false); }
		#endregion

		#region Methods written to be called via delegate.
		public void UpdateStatusText(string newText)
		{
			try
			{
				this.lblStatus.Text = newText;
			}
			catch (InvalidOperationException) { }
		}
		public void UpdateStatusStripText(string newText)
		{
			try
			{
				this.strStatus_Status.Text = newText;
			}
			catch (InvalidOperationException) { }
		}
		public void UpdateStatusStripProgress(int percentage)
		{
			try
			{
				if (percentage > this.strStatus_Progress.Maximum) percentage = this.strStatus_Progress.Maximum;
				if (percentage < this.strStatus_Progress.Minimum) percentage = this.strStatus_Progress.Minimum;
				this.strStatus_Progress.Value = percentage;
			}
			catch (InvalidOperationException) { }
		}
		public void ShowProgress()
		{
			try
			{
				this.strStatus_Progress.Visible = true;
			}
			catch (InvalidOperationException) { }
		}
		public void HideProgress()
		{
			try
			{
				this.strStatus_Progress.Visible = false;
			}
			catch (InvalidOperationException) { }
		}
		#endregion

		private void _statusLoopDownloading()
		{
			__UpdateStatusText ust = new __UpdateStatusText(this.UpdateStatusText);
			if (this._updatingStatus) return;

			try
			{
				int oldLength = this._downloader.QueueLength;
				while (this._running && this._downloader.QueueLength > 0)
				{
					this.Invoke(ust, "Waiting for background tasks: " + this._downloader.QueueLength + " downloads; " + this._downloader.DownloadSpeed + ".");
					Application.DoEvents();
					SysThread.Sleep(50);

					if (oldLength > this._downloader.QueueLength)
						this.Invoke(new MethodInvoker(this.UpdatePostDetails));
					oldLength = this._downloader.QueueLength;
				}

				this.Invoke(ust, "Ready.");
			}
#if !DEBUG
			catch { }
#endif
			finally { this._updatingStatus = false; }
		}

		#region Event Listeners
		#region Menu Items
		#region File
		private void mnuMain_FileNew_Click(object sender, EventArgs e)
		{
			Dialogs.frmNewDatabaseDialog d = new Dialogs.frmNewDatabaseDialog();
			DialogResult dr = d.ShowDialog();

			if (dr == DialogResult.OK)
			{
				ThreadDatabase db = new ThreadDatabase(d.DBName, d.DBLoc, d.DBUrl);

				if (!Directory.Exists(db.ImageDir))
					Directory.CreateDirectory(db.ImageDir);

				if (!d.ScrapeAll) db.CrawledAllPages = true;
				db.Save(); db.Dispose();
				this.LoadDatabase(d.DBLoc);

				if (d.StartNow) new SysThread(new ThreadStart(delegate() { this.mnuMain_ScraperNow_Click(null, null); })).Start();
			}
		}
		private void mnuMain_FileLoad_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.DefaultExt = ".db";
			ofd.Filter = "Database files (*.db)|*.db|All files (*.*)|*.*";
			ofd.FilterIndex = 0;
			ofd.Multiselect = false;
			ofd.RestoreDirectory = true;
			ofd.Title = "Select Database File";

			DialogResult dr = ofd.ShowDialog();
			if (dr == DialogResult.OK)
				this.LoadDatabase(ofd.FileName);
		}
		private void mnuMain_FileSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._db != null)
					this._db.Save();
			}
			catch (Exception ex)
			{
				Program._genericMessageBox("Error saving database: " + ex.GetType().Name + ": " + ex.Message, MessageBoxIcon.Error);
				return;
			}

			Program._genericMessageBox("Database successfully saved!", MessageBoxIcon.Information);
		}
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
			input.InputText = string.Format("{0}h{1}m{2}s", sp.Hours, sp.Minutes, sp.Seconds).Replace("0h", "").Replace("0m", "").Replace("0s", "");
			input.ShowDialog();

			string timestring = input.InputText.Trim();
			if (timestring == "")
				return;

			Regex h = new Regex("([0-9]*)h", RegexOptions.IgnoreCase), m = new Regex("([0-9]*)m", RegexOptions.IgnoreCase), s = new Regex("([0-9]*)s", RegexOptions.IgnoreCase);
			Match hm = h.Match(timestring), mm = m.Match(timestring), sm = s.Match(timestring);
			int newInterval = 0;

			if (hm.Success)
				newInterval += int.Parse(hm.Groups[1].Value) * 3600000;
			if (mm.Success)
				newInterval += int.Parse(mm.Groups[1].Value) * 60000;
			if (sm.Success)
				newInterval += int.Parse(sm.Groups[1].Value) * 1000;

			if (newInterval == 0)
				Program._genericMessageBox("There was an error parsing your input; ensure you specified a valid time string.", MessageBoxIcon.Exclamation);
			else
				this.timerAutoScrape.Interval = newInterval;
		}
		private void mnuMain_ScraperThreads_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this._downloaderThreads = this.mnuMain_ScraperThreads.SelectedIndex - 1;
		}
		private void mnuMain_ScraperMode_Metadata_Click(object sender, EventArgs e)
		{
			this.EnableScrapeImages = false;
		}
		private void mnuMain_ScraperMode_MetadataAndImages_Click(object sender, EventArgs e)
		{
			this.EnableScrapeImages = true;
		}
		private void mnuMain_ScraperManAdd_Click(object sender, EventArgs e)
		{
			Dialogs.frmInputDialog input = new Scraper.Dialogs.frmInputDialog("Enter the URL or thread ID of a thread in the board.");
			input.ShowDialog();

			string str = input.InputText.Trim();
			if (str == "")
				return;

			int id = 0;
			if (!int.TryParse(str, out id))
			{
				Match m = frmMain.threadIdR.Match(str);
				if (m.Success)
					id = int.Parse(m.Groups[1].Value);
			}

			if (id == 0)
				return;

			System.Net.HttpWebRequest req = System.Net.WebRequest.Create(this._db.URL + (this._db.URL.EndsWith("/") ? "" : "/") + "res/" + id) as System.Net.HttpWebRequest;
			req.Credentials = System.Net.CredentialCache.DefaultCredentials;
			req.Method = "HEAD";
			System.Net.HttpWebResponse resp = req.GetResponse() as System.Net.HttpWebResponse;
			if (resp.StatusCode != System.Net.HttpStatusCode.OK)
			{
				Program._genericMessageBox("The thread you specified was not found. Please check your input.", MessageBoxIcon.Exclamation); return;
			}
			resp.Close();

			if (this._threadParse != null && this._threadParse.IsAlive)
			{
				Program._genericMessageBox("A metadata scrape is already in progress. Please wait until the current metadata scrape is complete.", MessageBoxIcon.Warning); return;
			}

			Thread t = new Thread(id);
			this._threadParse = new SysThread(new ThreadStart(delegate()
			{
				this.Invoke(new __UpdateStatusText(this.UpdateStatusText), "Grabbing metadata for 1 thread...");
				try
				{
					using (BoardParser bp = new BoardParser(this._db.URL))
					{
						bp.CrawlThread(t);
					}
				}
				catch
				{
					Program._genericMessageBox("Crawling the thread failed. It may have been 404'd.", MessageBoxIcon.Error);
				}
			}));

			this._threadParse.Start();
			while (this._running && this._threadParse.IsAlive)
			{
				Application.DoEvents();
				SysThread.Sleep(50);
			}

			this._db.AddThread(t);
			this.DrawDatabaseTree(this._db);
			
			this._crawlThread(this._db[id], this._db.ImageDir);
			_statusLoopDownloading();
		}
		private void mnuMain_ScraperNow_Click(object sender, EventArgs e)
		{
			this.ScrapeBoard();
		}
		private void mnuMain_ScraperAll_Click(object sender, EventArgs e)
		{
			this.EnableScrapeAll = !this.EnableScrapeAll;
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

		#region Tree Context Menu
		void cmTree_Popup(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;
			if ((string) this.treePostWindowMouseAt.Tag == "thread")
			{
				this.cmTree.MenuItems.Clear();
				this.cmTree.MenuItems.AddRange(this.cmTree__Thread);

				this.cmTree_Delete.Text = "Delete Entire Thread (May be recrawled)";
				this.cmTree_Download.Text = "Download Thread Images";
			}
			else if ((string) this.treePostWindowMouseAt.Tag == "post")
			{
				this.cmTree.MenuItems.Clear();
				this.cmTree.MenuItems.AddRange(this.cmTree__Post);

				this.cmTree_Delete.Text = "Delete Post (May be recrawled)";
				this.cmTree_Download.Text = "Download Post Image";
			}
			else
			{ // Bug?
				this.cmTree.MenuItems.Clear();
			}
		}
		void cmTree_Rename_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			this.treePostWindow.LabelEdit = true;
			if (!this.treePostWindowMouseAt.IsEditing)
				this.treePostWindowMouseAt.BeginEdit();
		}
		void cmTree_Delete_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			if (!Program._genericConfimBox("Are you sure you want to delete " + this.treePostWindowMouseAt.Tag + " '" + this.treePostWindowMouseAt.Text + "'?", MessageBoxIcon.Question)) return;

			if (this.treePostWindowMouseAt.Tag.Equals("thread"))
			{
				Thread t = this._db[this.treePostWindowMouseAt.Text];
				foreach (Post p in t)
				{

					if (!p.ImagePath.Contains("http:"))
					{
						this._imageCache.Remove(p.ImagePath);
						try { File.Delete(p.ImagePath); }
						catch (Exception ex) { DebugConsole.ShowWarning("Exception thrown while deleting image for post ID " + p.Id + ": " + ex.GetType().ToString() + " " + ex.Message); }
					}
				}
				this._db.RemoveThread(t);
			}
			else if (this.treePostWindowMouseAt.Tag.Equals("post"))
			{
				Thread t = this._nodeToThread(this.treePostWindowMouseAt.Parent);
				Post p = this._nodeToPost(this.treePostWindowMouseAt);
				if (p == null || t == null) return; // Bug?

				if (!p.ImagePath.Contains("http:"))
				{
					this._imageCache.Remove(p.ImagePath);
					try { File.Delete(p.ImagePath); }
					catch (Exception ex) { DebugConsole.ShowWarning("Exception thrown while deleting image for post ID " + p.Id + ": " + ex.GetType().ToString() + " " + ex.Message); }
				}

				t.RemovePost(p);
			}

			//this.DrawDatabaseTree(this._db);
			this.UpdatePostDetails(null);
			this.treePostWindow.Nodes.Remove(this.treePostWindowMouseAt);
		}
		void cmTree_Rescrape_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			if (this.treePostWindowMouseAt.Tag.Equals("thread"))
			{
				if (this._threadParse != null && this._threadParse.IsAlive)
				{
					Program._genericMessageBox("A metadata scrape is already in progress. Please wait until the current metadata scrape is complete.", MessageBoxIcon.Warning); return;
				}

				Thread t = this._db[this.treePostWindowMouseAt.Text];
				if (t == null) return;

				this._threadParse = new SysThread(new ThreadStart(delegate()
				{
					this.Invoke(new __UpdateStatusText(this.UpdateStatusText), "Grabbing metadata for 1 thread...");
					try
					{
						using (BoardParser bp = new BoardParser(this._db.URL))
						{
							Thread tt = new Thread(t.Id);
							bp.CrawlThread(tt);
							t += tt;
						}
					}
					catch
					{
						Program._genericMessageBox("Crawling the thread failed. It may have been 404'd.", MessageBoxIcon.Error);
					}
					this._db[this.treePostWindowMouseAt.Text] = t;
				}));

				this._threadParse.Start();
				while (this._running && this._threadParse.IsAlive)
				{
					Application.DoEvents();
					SysThread.Sleep(50);
				}

				this.DrawDatabaseTree(this._db);
				this._crawlThread(t, this._db.ImageDir);
				_statusLoopDownloading();
			}
		}
		void cmTree_Download_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			if (this.treePostWindowMouseAt.Tag.Equals("post"))
			{
				Post p = this._nodeToPost(this.treePostWindowMouseAt);
				if (p == null && !p.ImagePath.Contains("http:")) return;

				this._downloader.QueuePost(this._db.ImageDir + @"\" + imgnameR.Match(p.ImagePath).Value, p);
				_statusLoopDownloading();
			}
			else if (this.treePostWindowMouseAt.Tag.Equals("thread"))
			{
				Thread t = this._nodeToThread(this.treePostWindowMouseAt);
				if (t == null) return;

				this._crawlThread(t, this._db.ImageDir, true);
				_statusLoopDownloading();
			}
		}
		void cmTree_OpenInExplorer_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			Post p = this._nodeToPost(this.treePostWindowMouseAt);
			if (p == null || p.ImagePath.Contains("http:")) return;

			System.Diagnostics.Process.Start("explorer.exe", "/select," + p.ImagePath);
		}
		void cmTree_GenerateImgUrl_Click(object sender, EventArgs e)
		{
			if (this.treePostWindowMouseAt == null) return;

			Post p = this._nodeToPost(this.treePostWindowMouseAt);
			if (p == null) return;

			if (p.ImagePath.Contains("http:"))
				Clipboard.SetText(p.ImagePath);
			else
			{
				Match m = boardnameR.Match(this._db.URL);
				Clipboard.SetText("http://images.4chan.org/" + m.Groups[1].Value + "/src/" + new FileInfo(p.ImagePath).Name);
			}
		}
		#endregion
		#region Tree View Events
		private void treePostWindow_MouseDown(object sender, MouseEventArgs e)
		{
			this.treePostWindowMouseAt = treePostWindow.GetNodeAt(e.X, e.Y);
			if (this.treePostWindowMouseAt == null) return;

			if (e.Button == MouseButtons.Left)
			{
				if (this.treePostWindowMouseAt.Tag.Equals("post"))
					this.UpdatePostDetails(this._nodeToPost(this.treePostWindowMouseAt));
				else
					this.UpdatePostDetails(this._nodeToPost(this.treePostWindowMouseAt.Nodes[0]));

				this.treePostWindow.SelectedNode = this.treePostWindowMouseAt;
			}
		}
		private void treePostWindow_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (this._tempNodeText == null)
				this._tempNodeText = e.Node.Text;
		}
		private void treePostWindow_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Label == null || e.Label.Length < 1 || e.CancelEdit)
			{
				e.Node.Text = this._tempNodeText; this._tempNodeText = null;
				e.CancelEdit = true;
				return;
			}
			if (this._db[e.Label] != null)
			{
				Program._genericMessageBox("A thread with that name already exists. Please choose another name.", MessageBoxIcon.Exclamation);

				e.Node.Text = this._tempNodeText; this._tempNodeText = null;
				e.CancelEdit = true;
				return;
			}

			this._db[this._tempNodeText].Name = e.Label;
			this._tempNodeText = null;
			this.treePostWindow.LabelEdit = false;
			this.DrawDatabaseTree(this._db);
		}
		private void treePostWindow_KeyUp(object sender, KeyEventArgs e)
		{
			if (this._tempNodeText != null && e.KeyCode == Keys.Escape)
			{
				this.treePostWindow.SelectedNode.Text = this._tempNodeText;
			}
			else if (this._tempNodeText == null && e.KeyCode == Keys.F2 && this.treePostWindow.SelectedNode != null && !this.treePostWindow.SelectedNode.Tag.Equals("post"))
			{
				this.treePostWindow.LabelEdit = true;
				if (!this.treePostWindow.SelectedNode.IsEditing)
					this.treePostWindow.SelectedNode.BeginEdit();
			}

			if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
			{
				this.treePostWindowMouseAt = this.treePostWindow.SelectedNode;

				if (this.treePostWindow.SelectedNode.Tag.Equals("post"))
					this.UpdatePostDetails(this._nodeToPost(this.treePostWindow.SelectedNode));
				else
					this.UpdatePostDetails(this._nodeToPost(this.treePostWindow.SelectedNode.Nodes[0]));
			}
			else if (e.KeyCode == Keys.Enter && this.treePostWindow.SelectedNode != null && "post".Equals(this.treePostWindow.SelectedNode.Tag))
			{
				Post p = this._nodeToPost(this.treePostWindow.SelectedNode);
				if (p.ImagePath.Contains("http:")) return;

				using (Dialogs.frmDetailDialog d = new Scraper.Dialogs.frmDetailDialog())
				{
					d.PostImage = this._getImage(p.ImagePath);
					d.PostString = p.PostBody;
					d.ShowDialog();
				}
			}
		}
		private void treePostWindow_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			if (e.Node.Tag.Equals("post"))
			{
				Post p = this._nodeToPost(this.treePostWindowMouseAt);
				if (p.ImagePath.Contains("http:")) return;

				using (Dialogs.frmDetailDialog d = new Scraper.Dialogs.frmDetailDialog())
				{
					d.PostImage = this._getImage(p.ImagePath);
					d.PostString = p.PostBody;
					d.ShowDialog();
				}
			}
		}
		#endregion

		void frmMain_Resize(object sender, System.EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized && this.mnuMain_FileMinimize.Checked)
				this.Hide();
		}
		void frmMain_ResizeEnd(object sender, System.EventArgs e)
		{
			if (this.WindowState != FormWindowState.Minimized)
				this.UpdatePostDetails();
		}
		void taskTrayIcon_DoubleClick(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState = FormWindowState.Normal;

			this.lblStatus.Text = "Ready.";

			this.TopMost = true;
			this.Focus();
			this.BringToFront();
			this.TopMost = false;
		}
		#endregion

		private Image _getImage(string path)
		{
			if (!this._imageCache.ContainsKey(path))
				this._imageCache.Add(path, new Bitmap(path));

			return this._imageCache[path];
		}
		private void _resizeImage(Image target, Image o)
		{
			int wd = target.Width, hd = target.Height;
			double wo = o.Width, ho = o.Height, t1 = wo / wd, t2 = ho / hd, scaleFactor = (t1 > 1 || t2 > 1) ? (1 / (t1 > t2 ? t1 : t2)) : 1;

			int wn = (int) (wo * scaleFactor), hn = (int) (ho * scaleFactor);
			if (wn < 1 || hn < 1) return;
			using (Graphics g = Graphics.FromImage(target))
			{
				using (Bitmap bb = new Bitmap(o, new Size(wn, hn)))
				{
					g.DrawImage(bb, new Point((wd - wn) / 2, (hd - hn) / 2));
				}
			}
		}

		private Post _nodeToPost(TreeNode n)
		{
			return this._db.FindPost(int.Parse(n.Text.Replace(" (OP)", "")));
		}
		private Thread _nodeToThread(TreeNode n)
		{
			return this._db[n.Text];
		}

		private class TreeViewComparer : System.Collections.IComparer
		{
			public int Compare(object x, object y)
			{
				if (x.GetType() != typeof(TreeNode) || y.GetType() != typeof(TreeNode))
					throw new ArgumentException();

				TreeNode xx = (TreeNode) x, yy = (TreeNode) y;
				if ("thread".Equals(xx.Tag) && "thread".Equals(yy.Tag))
					if (char.IsLetter(xx.Text[0]) && char.IsLetter(yy.Text[0]))
						return xx.Text.CompareTo(yy.Text);
					else
						return -xx.Text.CompareTo(yy.Text);
				else
					return xx.Text.CompareTo(yy.Text);
			}
		}
	}
}
