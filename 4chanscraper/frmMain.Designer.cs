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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.mnuMain = new System.Windows.Forms.MenuStrip();
			this.mnuMain_File = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_FileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_FileLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_FileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_FileMinimize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_FileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_Scraper = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperEnabled = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperConf = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ScraperThreads = new System.Windows.Forms.ToolStripComboBox();
			this.mnuMain_ScraperMode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperMode_Metadata = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperMode_MetadataAndImages = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuMain_ScraperManAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_ScraperNow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuMain_HelpDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.treePostWindow = new System.Windows.Forms.TreeView();
			this.grpStatus = new System.Windows.Forms.GroupBox();
			this.pnlDetails = new System.Windows.Forms.Panel();
			this.grpPostStats = new System.Windows.Forms.GroupBox();
			this.lblPostImgInfo = new System.Windows.Forms.Label();
			this.lblPostImgPath = new System.Windows.Forms.Label();
			this.lblPostDate = new System.Windows.Forms.Label();
			this.picPostImg = new System.Windows.Forms.PictureBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.grpDbStats = new System.Windows.Forms.GroupBox();
			this.lblDataImg = new System.Windows.Forms.Label();
			this.lblDataText = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblPostCount_Downloaded = new System.Windows.Forms.Label();
			this.lblPostCount_Metadata = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lblThreadCount_Downloaded = new System.Windows.Forms.Label();
			this.lblThreadCount_Metadata = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblStatus = new System.Windows.Forms.Label();
			this.taskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmTaskTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cmTaskTray_Show = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmTaskTray_Enabled = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.cmTaskTray_Close = new System.Windows.Forms.ToolStripMenuItem();
			this.timerAutoScrape = new System.Windows.Forms.Timer(this.components);
			this.strStatus = new System.Windows.Forms.StatusStrip();
			this.strStatus_Status = new System.Windows.Forms.ToolStripStatusLabel();
			this.strStatus_Progress = new System.Windows.Forms.ToolStripProgressBar();
			this.frmMain_ToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.mnuMain.SuspendLayout();
			this.grpStatus.SuspendLayout();
			this.pnlDetails.SuspendLayout();
			this.grpPostStats.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.picPostImg)).BeginInit();
			this.grpDbStats.SuspendLayout();
			this.cmTaskTray.SuspendLayout();
			this.strStatus.SuspendLayout();
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
            this.toolStripSeparator2,
            this.mnuMain_FileLoad,
            this.mnuMain_FileSave,
            this.toolStripSeparator3,
            this.mnuMain_FileMinimize,
            this.mnuMain_FileExit});
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
			this.mnuMain_FileNew.Click += new System.EventHandler(this.mnuMain_FileNew_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuMain_FileLoad
			// 
			this.mnuMain_FileLoad.Name = "mnuMain_FileLoad";
			this.mnuMain_FileLoad.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.mnuMain_FileLoad.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileLoad.Text = "Load Database";
			this.mnuMain_FileLoad.Click += new System.EventHandler(this.mnuMain_FileLoad_Click);
			// 
			// mnuMain_FileSave
			// 
			this.mnuMain_FileSave.Name = "mnuMain_FileSave";
			this.mnuMain_FileSave.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuMain_FileSave.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileSave.Text = "Save Database";
			this.mnuMain_FileSave.Click += new System.EventHandler(this.mnuMain_FileSave_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(189, 6);
			// 
			// mnuMain_FileMinimize
			// 
			this.mnuMain_FileMinimize.Checked = true;
			this.mnuMain_FileMinimize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuMain_FileMinimize.Name = "mnuMain_FileMinimize";
			this.mnuMain_FileMinimize.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileMinimize.Text = "Minimize to Tray";
			this.mnuMain_FileMinimize.Click += new System.EventHandler(this.mnuMain_FileMinimize_Click);
			// 
			// mnuMain_FileExit
			// 
			this.mnuMain_FileExit.Name = "mnuMain_FileExit";
			this.mnuMain_FileExit.ShortcutKeys = ((System.Windows.Forms.Keys) ((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.mnuMain_FileExit.Size = new System.Drawing.Size(192, 22);
			this.mnuMain_FileExit.Text = "Exit";
			this.mnuMain_FileExit.Click += new System.EventHandler(this.mnuMain_FileExit_Click);
			// 
			// mnuMain_Scraper
			// 
			this.mnuMain_Scraper.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_ScraperEnabled,
            this.mnuMain_ScraperConf,
            this.toolStripSeparator4,
            this.mnuMain_ScraperThreads,
            this.mnuMain_ScraperMode,
            this.mnuMain_ScraperAll,
            this.toolStripSeparator6,
            this.mnuMain_ScraperManAdd,
            this.mnuMain_ScraperNow});
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
			// mnuMain_ScraperConf
			// 
			this.mnuMain_ScraperConf.Name = "mnuMain_ScraperConf";
			this.mnuMain_ScraperConf.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperConf.Text = "Configure Scrape Interval";
			this.mnuMain_ScraperConf.Click += new System.EventHandler(this.mnuMain_ScraperConf_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(207, 6);
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
			this.mnuMain_ScraperThreads.Visible = false;
			this.mnuMain_ScraperThreads.SelectedIndexChanged += new System.EventHandler(this.mnuMain_ScraperThreads_SelectedIndexChanged);
			// 
			// mnuMain_ScraperMode
			// 
			this.mnuMain_ScraperMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMain_ScraperMode_Metadata,
            this.mnuMain_ScraperMode_MetadataAndImages});
			this.mnuMain_ScraperMode.Name = "mnuMain_ScraperMode";
			this.mnuMain_ScraperMode.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperMode.Text = "Scrape Mode";
			// 
			// mnuMain_ScraperMode_Metadata
			// 
			this.mnuMain_ScraperMode_Metadata.Name = "mnuMain_ScraperMode_Metadata";
			this.mnuMain_ScraperMode_Metadata.Size = new System.Drawing.Size(188, 22);
			this.mnuMain_ScraperMode_Metadata.Text = "Metadata only";
			this.mnuMain_ScraperMode_Metadata.Click += new System.EventHandler(this.mnuMain_ScraperMode_Metadata_Click);
			// 
			// mnuMain_ScraperMode_MetadataAndImages
			// 
			this.mnuMain_ScraperMode_MetadataAndImages.Checked = true;
			this.mnuMain_ScraperMode_MetadataAndImages.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuMain_ScraperMode_MetadataAndImages.Name = "mnuMain_ScraperMode_MetadataAndImages";
			this.mnuMain_ScraperMode_MetadataAndImages.Size = new System.Drawing.Size(188, 22);
			this.mnuMain_ScraperMode_MetadataAndImages.Text = "Metadata and Images";
			this.mnuMain_ScraperMode_MetadataAndImages.Click += new System.EventHandler(this.mnuMain_ScraperMode_MetadataAndImages_Click);
			// 
			// mnuMain_ScraperAll
			// 
			this.mnuMain_ScraperAll.Name = "mnuMain_ScraperAll";
			this.mnuMain_ScraperAll.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperAll.Text = "Scrape All Pages";
			this.mnuMain_ScraperAll.Click += new System.EventHandler(this.mnuMain_ScraperAll_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(207, 6);
			// 
			// mnuMain_ScraperManAdd
			// 
			this.mnuMain_ScraperManAdd.Name = "mnuMain_ScraperManAdd";
			this.mnuMain_ScraperManAdd.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperManAdd.Text = "Manual Thread Add";
			this.mnuMain_ScraperManAdd.Click += new System.EventHandler(this.mnuMain_ScraperManAdd_Click);
			// 
			// mnuMain_ScraperNow
			// 
			this.mnuMain_ScraperNow.Name = "mnuMain_ScraperNow";
			this.mnuMain_ScraperNow.Size = new System.Drawing.Size(210, 22);
			this.mnuMain_ScraperNow.Text = "Manual Scrape Now";
			this.mnuMain_ScraperNow.Click += new System.EventHandler(this.mnuMain_ScraperNow_Click);
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
			this.mnuMain_HelpAbout.Size = new System.Drawing.Size(149, 22);
			this.mnuMain_HelpAbout.Text = "About";
			this.mnuMain_HelpAbout.Click += new System.EventHandler(this.mnuMain_HelpAbout_Click);
			// 
			// mnuMain_HelpDebug
			// 
			this.mnuMain_HelpDebug.Name = "mnuMain_HelpDebug";
			this.mnuMain_HelpDebug.Size = new System.Drawing.Size(149, 22);
			this.mnuMain_HelpDebug.Text = "Show Console";
			this.mnuMain_HelpDebug.Click += new System.EventHandler(this.mnuMain_HelpDebug_Click);
			// 
			// treePostWindow
			// 
			this.treePostWindow.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.treePostWindow.Location = new System.Drawing.Point(12, 27);
			this.treePostWindow.Name = "treePostWindow";
			this.treePostWindow.Size = new System.Drawing.Size(227, 352);
			this.treePostWindow.TabIndex = 1;
			this.treePostWindow.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treePostWindow_NodeMouseDoubleClick);
			this.treePostWindow.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treePostWindow_AfterLabelEdit);
			this.treePostWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treePostWindow_MouseDown);
			this.treePostWindow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treePostWindow_KeyUp);
			this.treePostWindow.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treePostWindow_BeforeLabelEdit);
			// 
			// grpStatus
			// 
			this.grpStatus.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
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
			this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlDetails.Controls.Add(this.grpPostStats);
			this.pnlDetails.Controls.Add(this.grpDbStats);
			this.pnlDetails.Location = new System.Drawing.Point(9, 32);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(304, 314);
			this.pnlDetails.TabIndex = 1;
			this.pnlDetails.Visible = false;
			// 
			// grpPostStats
			// 
			this.grpPostStats.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpPostStats.Controls.Add(this.lblPostImgInfo);
			this.grpPostStats.Controls.Add(this.lblPostImgPath);
			this.grpPostStats.Controls.Add(this.lblPostDate);
			this.grpPostStats.Controls.Add(this.picPostImg);
			this.grpPostStats.Controls.Add(this.label11);
			this.grpPostStats.Controls.Add(this.label10);
			this.grpPostStats.Controls.Add(this.label9);
			this.grpPostStats.Location = new System.Drawing.Point(0, 69);
			this.grpPostStats.Name = "grpPostStats";
			this.grpPostStats.Size = new System.Drawing.Size(304, 245);
			this.grpPostStats.TabIndex = 2;
			this.grpPostStats.TabStop = false;
			this.grpPostStats.Text = "Post Info";
			// 
			// lblPostImgInfo
			// 
			this.lblPostImgInfo.AutoSize = true;
			this.lblPostImgInfo.Location = new System.Drawing.Point(62, 42);
			this.lblPostImgInfo.Name = "lblPostImgInfo";
			this.lblPostImgInfo.Size = new System.Drawing.Size(104, 13);
			this.lblPostImgInfo.TabIndex = 6;
			this.lblPostImgInfo.Text = "1337 KB (1024x768)";
			// 
			// lblPostImgPath
			// 
			this.lblPostImgPath.AutoSize = true;
			this.lblPostImgPath.Location = new System.Drawing.Point(62, 29);
			this.lblPostImgPath.Name = "lblPostImgPath";
			this.lblPostImgPath.Size = new System.Drawing.Size(89, 13);
			this.lblPostImgPath.TabIndex = 5;
			this.lblPostImgPath.Text = "123456\\3463.jpg";
			// 
			// lblPostDate
			// 
			this.lblPostDate.AutoSize = true;
			this.lblPostDate.Location = new System.Drawing.Point(62, 16);
			this.lblPostDate.Name = "lblPostDate";
			this.lblPostDate.Size = new System.Drawing.Size(109, 13);
			this.lblPostDate.TabIndex = 4;
			this.lblPostDate.Text = "Jan 1, 1970 12:00AM";
			// 
			// picPostImg
			// 
			this.picPostImg.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.picPostImg.Location = new System.Drawing.Point(3, 59);
			this.picPostImg.Name = "picPostImg";
			this.picPostImg.Size = new System.Drawing.Size(298, 180);
			this.picPostImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picPostImg.TabIndex = 3;
			this.picPostImg.TabStop = false;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(2, 29);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(63, 13);
			this.label11.TabIndex = 2;
			this.label11.Text = "Image path:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 42);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(59, 13);
			this.label10.TabIndex = 1;
			this.label10.Text = "Image info:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(32, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(33, 13);
			this.label9.TabIndex = 0;
			this.label9.Text = "Date:";
			// 
			// grpDbStats
			// 
			this.grpDbStats.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.grpDbStats.Controls.Add(this.lblDataImg);
			this.grpDbStats.Controls.Add(this.lblDataText);
			this.grpDbStats.Controls.Add(this.label6);
			this.grpDbStats.Controls.Add(this.label5);
			this.grpDbStats.Controls.Add(this.lblPostCount_Downloaded);
			this.grpDbStats.Controls.Add(this.lblPostCount_Metadata);
			this.grpDbStats.Controls.Add(this.label7);
			this.grpDbStats.Controls.Add(this.label8);
			this.grpDbStats.Controls.Add(this.label4);
			this.grpDbStats.Controls.Add(this.lblThreadCount_Downloaded);
			this.grpDbStats.Controls.Add(this.lblThreadCount_Metadata);
			this.grpDbStats.Controls.Add(this.label3);
			this.grpDbStats.Controls.Add(this.label2);
			this.grpDbStats.Controls.Add(this.label1);
			this.grpDbStats.Location = new System.Drawing.Point(0, 0);
			this.grpDbStats.Name = "grpDbStats";
			this.grpDbStats.Size = new System.Drawing.Size(304, 70);
			this.grpDbStats.TabIndex = 1;
			this.grpDbStats.TabStop = false;
			this.grpDbStats.Text = "Database Statistics";
			// 
			// lblDataImg
			// 
			this.lblDataImg.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDataImg.Location = new System.Drawing.Point(156, 53);
			this.lblDataImg.Name = "lblDataImg";
			this.lblDataImg.Size = new System.Drawing.Size(56, 13);
			this.lblDataImg.TabIndex = 13;
			this.lblDataImg.Text = "00000000";
			this.lblDataImg.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblDataText
			// 
			this.lblDataText.Location = new System.Drawing.Point(18, 53);
			this.lblDataText.Name = "lblDataText";
			this.lblDataText.Size = new System.Drawing.Size(56, 13);
			this.lblDataText.TabIndex = 12;
			this.lblDataText.Text = "00000000";
			this.lblDataText.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(208, 53);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(81, 13);
			this.label6.TabIndex = 11;
			this.label6.Text = "MB Image Data";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(70, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "MB Text Data";
			// 
			// lblPostCount_Downloaded
			// 
			this.lblPostCount_Downloaded.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPostCount_Downloaded.AutoSize = true;
			this.lblPostCount_Downloaded.Location = new System.Drawing.Point(246, 29);
			this.lblPostCount_Downloaded.Name = "lblPostCount_Downloaded";
			this.lblPostCount_Downloaded.Size = new System.Drawing.Size(37, 13);
			this.lblPostCount_Downloaded.TabIndex = 9;
			this.lblPostCount_Downloaded.Text = "00000";
			// 
			// lblPostCount_Metadata
			// 
			this.lblPostCount_Metadata.AutoSize = true;
			this.lblPostCount_Metadata.Location = new System.Drawing.Point(138, 29);
			this.lblPostCount_Metadata.Name = "lblPostCount_Metadata";
			this.lblPostCount_Metadata.Size = new System.Drawing.Size(37, 13);
			this.lblPostCount_Metadata.TabIndex = 8;
			this.lblPostCount_Metadata.Text = "00000";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(180, 29);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Downloaded:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(87, 29);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(55, 13);
			this.label8.TabIndex = 6;
			this.label8.Text = "Metadata:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Post Count:";
			// 
			// lblThreadCount_Downloaded
			// 
			this.lblThreadCount_Downloaded.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblThreadCount_Downloaded.AutoSize = true;
			this.lblThreadCount_Downloaded.Location = new System.Drawing.Point(246, 16);
			this.lblThreadCount_Downloaded.Name = "lblThreadCount_Downloaded";
			this.lblThreadCount_Downloaded.Size = new System.Drawing.Size(37, 13);
			this.lblThreadCount_Downloaded.TabIndex = 4;
			this.lblThreadCount_Downloaded.Text = "00000";
			// 
			// lblThreadCount_Metadata
			// 
			this.lblThreadCount_Metadata.AutoSize = true;
			this.lblThreadCount_Metadata.Location = new System.Drawing.Point(138, 16);
			this.lblThreadCount_Metadata.Name = "lblThreadCount_Metadata";
			this.lblThreadCount_Metadata.Size = new System.Drawing.Size(37, 13);
			this.lblThreadCount_Metadata.TabIndex = 3;
			this.lblThreadCount_Metadata.Text = "00000";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(180, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Downloaded:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(87, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Metadata:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Thread Count:";
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
            this.toolStripSeparator5,
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
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(196, 6);
			// 
			// cmTaskTray_Close
			// 
			this.cmTaskTray_Close.Name = "cmTaskTray_Close";
			this.cmTaskTray_Close.Size = new System.Drawing.Size(199, 22);
			this.cmTaskTray_Close.Text = "Close 4chanscraper";
			this.cmTaskTray_Close.Click += new System.EventHandler(this.cmTaskTray_Close_Click);
			// 
			// timerAutoScrape
			// 
			this.timerAutoScrape.Interval = 120000;
			// 
			// strStatus
			// 
			this.strStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.strStatus_Status,
            this.strStatus_Progress});
			this.strStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.strStatus.Location = new System.Drawing.Point(0, 382);
			this.strStatus.Name = "strStatus";
			this.strStatus.Size = new System.Drawing.Size(580, 22);
			this.strStatus.TabIndex = 3;
			// 
			// strStatus_Status
			// 
			this.strStatus_Status.Name = "strStatus_Status";
			this.strStatus_Status.Size = new System.Drawing.Size(42, 17);
			this.strStatus_Status.Text = "Ready.";
			// 
			// strStatus_Progress
			// 
			this.strStatus_Progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.strStatus_Progress.Name = "strStatus_Progress";
			this.strStatus_Progress.Size = new System.Drawing.Size(100, 16);
			this.strStatus_Progress.Value = 50;
			this.strStatus_Progress.Visible = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(580, 404);
			this.Controls.Add(this.strStatus);
			this.Controls.Add(this.grpStatus);
			this.Controls.Add(this.treePostWindow);
			this.Controls.Add(this.mnuMain);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mnuMain;
			this.MinimumSize = new System.Drawing.Size(596, 442);
			this.Name = "frmMain";
			this.Text = "4chanscraper";
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.ResizeEnd += new System.EventHandler(this.frmMain_ResizeEnd);
			this.mnuMain.ResumeLayout(false);
			this.mnuMain.PerformLayout();
			this.grpStatus.ResumeLayout(false);
			this.grpStatus.PerformLayout();
			this.pnlDetails.ResumeLayout(false);
			this.grpPostStats.ResumeLayout(false);
			this.grpPostStats.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.picPostImg)).EndInit();
			this.grpDbStats.ResumeLayout(false);
			this.grpDbStats.PerformLayout();
			this.cmTaskTray.ResumeLayout(false);
			this.strStatus.ResumeLayout(false);
			this.strStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mnuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_File;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileNew;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileLoad;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileSave;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileMinimize;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_FileExit;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Scraper;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperEnabled;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperConf;
		private System.Windows.Forms.ToolStripComboBox mnuMain_ScraperThreads;
		private System.Windows.Forms.TreeView treePostWindow;
		private System.Windows.Forms.GroupBox grpStatus;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_Help;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_HelpAbout;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_HelpDebug;
		private System.Windows.Forms.Panel pnlDetails;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.NotifyIcon taskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip cmTaskTray;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Enabled;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Show;
		private System.Windows.Forms.ToolStripMenuItem cmTaskTray_Close;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.GroupBox grpDbStats;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblThreadCount_Metadata;
		private System.Windows.Forms.Label lblThreadCount_Downloaded;
		private System.Windows.Forms.Label lblPostCount_Downloaded;
		private System.Windows.Forms.Label lblPostCount_Metadata;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lblDataImg;
		private System.Windows.Forms.Label lblDataText;
		private System.Windows.Forms.GroupBox grpPostStats;
		private System.Windows.Forms.PictureBox picPostImg;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblPostDate;
		private System.Windows.Forms.Label lblPostImgInfo;
		private System.Windows.Forms.Label lblPostImgPath;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperNow;
		private System.Windows.Forms.Timer timerAutoScrape;
		private System.Windows.Forms.StatusStrip strStatus;
		private System.Windows.Forms.ToolStripStatusLabel strStatus_Status;
		private System.Windows.Forms.ToolStripProgressBar strStatus_Progress;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperMode;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperMode_Metadata;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperMode_MetadataAndImages;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperAll;
		private System.Windows.Forms.ToolTip frmMain_ToolTip;
		private System.Windows.Forms.ToolStripMenuItem mnuMain_ScraperManAdd;
	}
}

