namespace Scraper
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Node5");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode5});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.mnuMain_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_FileLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_FileMinimize = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_Scraper = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperEnabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ScraperConf = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperThreads = new System.Windows.Forms.ToolStripComboBox();
			this.mnuMain_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_HelpDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.grpStatus = new System.Windows.Forms.GroupBox();
			this.pnlDetails = new System.Windows.Forms.Panel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.taskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmTaskTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmTaskTray_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmTaskTray_Enabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.cmTaskTray_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain.SuspendLayout();
			this.grpStatus.SuspendLayout();
			this.cmTaskTray.SuspendLayout();
			this.SuspendLayout();
			// 
			// mnuMain
			// 
			this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_File,
            this.mnuMain_Scraper,
            this.mnuMain_Help});
			this.mnuMain.Location = new System.Drawing.Point(0, 0);
			this.mnuMain.Name = "mnuMain";
			this.mnuMain.Size = new System.Drawing.Size(580, 24);
			this.mnuMain.TabIndex = 0;
			this.mnuMain.Text = "menuStrip1";
			// 
			// mnuMain_File
			// 
			this.mnuMain_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_FileNew,
            this.toolStripMenuItem1,
            this.mnuMain_FileLoad,
            this.mnuMain_FileSave,
            this.toolStripMenuItem2,
            this.mnuMain_FileMinimize,
            this.exitToolStripMenuItem});
			this.mnuMain_File.Name = "mnuMain_File";
			this.mnuMain_File.Size = new System.Drawing.Size(37, 20);
			this.mnuMain_File.Text = "File";
			// 
			// mnuMain_FileNew
			// 
			this.mnuMain_FileNew.Name = "mnuMain_FileNew";
			this.mnuMain_FileNew.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuMain_FileNew.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileNew.Text = "New Database";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuMain_FileLoad
			// 
			this.mnuMain_FileLoad.Name = "mnuMain_FileLoad";
			this.mnuMain_FileLoad.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.mnuMain_FileLoad.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileLoad.Text = "Load Database";
			// 
			// mnuMain_FileSave
			// 
			this.mnuMain_FileSave.Name = "mnuMain_FileSave";
			this.mnuMain_FileSave.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuMain_FileSave.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileSave.Text = "Save Database";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuMain_FileMinimize
			// 
			this.mnuMain_FileMinimize.Checked = true;
			this.mnuMain_FileMinimize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuMain_FileMinimize.Name = "mnuMain_FileMinimize";
			this.mnuMain_FileMinimize.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileMinimize.Text = "Minimize to Tray";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// mnuMain_Scraper
			// 
			this.mnuMain_Scraper.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_ScraperEnabled,
            this.toolStripMenuItem3,
            this.mnuMain_ScraperConf,
            this.mnuMain_ScraperThreads});
			this.mnuMain_Scraper.Name = "mnuMain_Scraper";
			this.mnuMain_Scraper.Size = new System.Drawing.Size(101, 20);
			this.mnuMain_Scraper.Text = "Scraper Control";
			// 
			// mnuMain_ScraperEnabled
			// 
			this.mnuMain_ScraperEnabled.Name = "mnuMain_ScraperEnabled";
			this.mnuMain_ScraperEnabled.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperEnabled.Text = "Enable Auto-Scrape";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuMain_ScraperConf
			// 
			this.mnuMain_ScraperConf.Name = "mnuMain_ScraperConf";
			this.mnuMain_ScraperConf.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperConf.Text = "Configure Scrape Interval";
			// 
			// mnuMain_ScraperThreads
			// 
			this.mnuMain_ScraperThreads.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.mnuMain_ScraperThreads.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.mnuMain_ScraperThreads.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.mnuMain_ScraperThreads.Items.AddRange(new object[] {
            "Download Threads: 1",
            "Download Threads: 2",
            "Download Threads: 3",
            "Download Threads: 4"});
			this.mnuMain_ScraperThreads.Name = "mnuMain_ScraperThreads";
			this.mnuMain_ScraperThreads.Size = new System.Drawing.Size(150, 23);
			this.mnuMain_ScraperThreads.SelectedIndexChanged += new System.EventHandler(this.mnuMain_ScraperThreads_SelectedIndexChanged);
			// 
			// mnuMain_Help
			// 
			this.mnuMain_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_HelpAbout,
            this.mnuMain_HelpDebug});
			this.mnuMain_Help.Name = "mnuMain_Help";
			this.mnuMain_Help.Size = new System.Drawing.Size(44, 20);
			this.mnuMain_Help.Text = "Help";
			// 
			// mnuMain_HelpAbout
			// 
			this.mnuMain_HelpAbout.Name = "mnuMain_HelpAbout";
			this.mnuMain_HelpAbout.Size = new System.Drawing.Size(152, 22);
			this.mnuMain_HelpAbout.Text = "About";
			this.mnuMain_HelpAbout.Click += new System.EventHandler(this.mnuMain_HelpAbout_Click);
			// 
			// mnuMain_HelpDebug
			// 
			this.mnuMain_HelpDebug.Name = "mnuMain_HelpDebug";
			this.mnuMain_HelpDebug.Size = new System.Drawing.Size(152, 22);
			this.mnuMain_HelpDebug.Text = "Show Console";
			this.mnuMain_HelpDebug.Click += new System.EventHandler(this.mnuMain_HelpDebug_Click);
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 27);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "Node1";
			treeNode1.Text = "Node1";
			treeNode2.Name = "Node2";
			treeNode2.Text = "Node2";
			treeNode3.Name = "Node3";
			treeNode3.Text = "Node3";
			treeNode4.Name = "Node0";
			treeNode4.Text = "Node0";
			treeNode5.Name = "Node5";
			treeNode5.Text = "Node5";
			treeNode6.Name = "Node4";
			treeNode6.Text = "Node4";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode6});
			this.treeView1.Size = new System.Drawing.Size(227, 352);
			this.treeView1.TabIndex = 1;
			// 
			// grpStatus
			// 
			this.grpStatus.Controls.Add(this.pnlDetails);
			this.grpStatus.Controls.Add(this.lblStatus);
			this.grpStatus.Location = new System.Drawing.Point(247, 21);
			this.grpStatus.Name = "grpStatus";
			this.grpStatus.Size = new System.Drawing.Size(323, 359);
			this.grpStatus.TabIndex = 2;
			this.grpStatus.TabStop = false;
			this.grpStatus.Text = "Status";
			// 
			// pnlDetails
			// 
			this.pnlDetails.Location = new System.Drawing.Point(9, 32);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(304, 314);
			this.pnlDetails.TabIndex = 1;
			this.pnlDetails.Visible = false;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(6, 16);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(207, 13);
			this.lblStatus.TabIndex = 0;
			this.lblStatus.Text = "Please create or load a database to begin.";
			// 
			// taskTrayIcon
			// 
			this.taskTrayIcon.ContextMenuStrip = this.cmTaskTray;
			this.taskTrayIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("taskTrayIcon.Icon")));
			this.taskTrayIcon.Text = "4chanscraper";
			this.taskTrayIcon.Visible = true;
			this.taskTrayIcon.DoubleClick += new System.EventHandler(this.taskTrayIcon_DoubleClick);
			// 
			// cmTaskTray
			// 
			this.cmTaskTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmTaskTray_Show,
            this.toolStripSeparator1,
            this.cmTaskTray_Enabled,
            this.toolStripMenuItem4,
            this.cmTaskTray_Close});
			this.cmTaskTray.Name = "cmTaskTray";
			this.cmTaskTray.Size = new System.Drawing.Size(200, 82);
			// 
			// cmTaskTray_Show
			// 
			this.cmTaskTray_Show.Name = "cmTaskTray_Show";
			this.cmTaskTray_Show.Size = new System.Drawing.Size(199, 22);
			this.cmTaskTray_Show.Text = "Show Program Window";
			this.cmTaskTray_Show.Click += new System.EventHandler(this.cmTaskTray_Show_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
			// 
			// cmTaskTray_Enabled
			// 
			this.cmTaskTray_Enabled.Name = "cmTaskTray_Enabled";
			this.cmTaskTray_Enabled.Size = new System.Drawing.Size(199, 22);
			this.cmTaskTray_Enabled.Text = "Enable Auto-Scrape";
			this.cmTaskTray_Enabled.Click += new System.EventHandler(this.cmTaskTray_Enabled_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(196, 6);
			// 
			// cmTaskTray_Close
			// 
			this.cmTaskTray_Close.Name = "cmTaskTray_Close";
			this.cmTaskTray_Close.Size = new System.Drawing.Size(199, 22);
			this.cmTaskTray_Close.Text = "Close 4chanscraper";
			this.cmTaskTray_Close.Click += new System.EventHandler(this.cmTaskTray_Close_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 391);
			this.Controls.Add(this.grpStatus);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.Name = "frmMain";
			this.Text = "4chanscraper";
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.grpStatus.ResumeLayout(false);
			this.grpStatus.PerformLayout();
			this.cmTaskTray.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileNew;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileLoad;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileMinimize;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Scraper;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperEnabled;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperConf;
		private System.Windows.Forms.ToolStripComboBox mnuMain_ScraperThreads;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.GroupBox grpStatus;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Help;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_HelpAbout;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_HelpDebug;
		private System.Windows.Forms.Panel pnlDetails;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.NotifyIcon taskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip cmTaskTray;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Enabled;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Show;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Close;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}

