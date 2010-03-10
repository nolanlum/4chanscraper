namespace Scraper.Dialogs
{
	partial class frmDetailDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetailDialog));
			this.btnClose = new System.Windows.Forms.Button();
			this.picPicture = new System.Windows.Forms.PictureBox();
			this.lblPost = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) (this.picPicture)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(81, 273);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// picPicture
			// 
			this.picPicture.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.picPicture.Location = new System.Drawing.Point(12, 12);
			this.picPicture.Name = "picPicture";
			this.picPicture.Size = new System.Drawing.Size(215, 215);
			this.picPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picPicture.TabIndex = 1;
			this.picPicture.TabStop = false;
			// 
			// lblPost
			// 
			this.lblPost.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPost.Location = new System.Drawing.Point(12, 230);
			this.lblPost.Name = "lblPost";
			this.lblPost.Size = new System.Drawing.Size(215, 43);
			this.lblPost.TabIndex = 2;
			this.lblPost.Text = "The text of the post goes here. This is a long winded post that probably would ne" +
				"ver be posted IRL but is placed here to demonstrate text wrapping capabilities.";
			this.lblPost.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// frmDetailDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(239, 304);
			this.Controls.Add(this.lblPost);
			this.Controls.Add(this.picPicture);
			this.Controls.Add(this.btnClose);
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.Name = "frmDetailDialog";
			this.Text = "Post Detail View";
			((System.ComponentModel.ISupportInitialize) (this.picPicture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.PictureBox picPicture;
		private System.Windows.Forms.Label lblPost;
	}
}