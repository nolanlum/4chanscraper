using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Scraper.Dialogs
{
	public partial class frmDetailDialog : Form
	{
		const int WM_SIZING = 0x214;
		const int WMSZ_LEFT = 1;
		const int WMSZ_RIGHT = 2;
		const int WMSZ_TOP = 3;
		const int WMSZ_BOTTOM = 6;
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		private double widthRatio = 4;
		private double heightRatio = 3;

		public Image PostImage
		{
			get { return this.picPicture.Tag as Image; }
			set { this.picPicture.Tag = value; this.widthRatio = value.Width; this.heightRatio = value.Height; this.ResizeImageBestFit(); }
		}
		public string PostString
		{
			get { return this.lblPost.Text; }
			set { this.lblPost.Text = value; }
		}

		public frmDetailDialog()
		{
			InitializeComponent();

			this.picPicture.Tag = new Bitmap(640, 480);
		}

		public void ResizeImageBestFit()
		{
			Size max = SystemInformation.PrimaryMonitorSize;
			Size twenty = new Size((int) (this.PostImage.Width * .2), (int) (this.PostImage.Height * .2));
			Image newI = (Image) this.PostImage.Clone();

			while(newI.Width >= max.Width || newI.Height >= max.Height)
				newI = new Bitmap(newI, newI.Size - twenty);

			this.picPicture.Image = newI;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_SIZING)
			{
				RECT rc = (RECT) Marshal.PtrToStructure(m.LParam, typeof(RECT));
				int res = m.WParam.ToInt32();
				if (res == WMSZ_LEFT || res == WMSZ_RIGHT)
				{
					//Left or right resize -> adjust height (bottom)
					rc.Bottom = rc.Top + (int) (heightRatio * this.Width / widthRatio);
				}
				else if (res == WMSZ_TOP || res == WMSZ_BOTTOM)
				{
					//Up or down resize -> adjust width (right)
					rc.Right = rc.Left + (int) (widthRatio * this.Height / heightRatio);
				}
				else if (res == WMSZ_RIGHT + WMSZ_BOTTOM)
				{
					//Lower-right corner resize -> adjust height (could have been width)
					rc.Bottom = rc.Top + (int) (heightRatio * this.Width / widthRatio);
				}
				else if (res == WMSZ_LEFT + WMSZ_TOP)
				{
					//Upper-left corner -> adjust width (could have been height)
					rc.Left = rc.Right - (int) (widthRatio * this.Height / heightRatio);
				}
				Marshal.StructureToPtr(rc, m.LParam, true);
			}

			base.WndProc(ref m);
		}
	}
}
