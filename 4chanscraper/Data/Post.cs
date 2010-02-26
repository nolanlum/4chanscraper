using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace Scraper.Data
{
	[Serializable]
	class Post
	{
		#region Private Members
		private int id;
		private string body;
		private string imgName;
		private DateTime time;
		[NonSerialized] private Bitmap imgBmap;
		#endregion

		#region Public Properties
		public int Id
		{
			get { return this.id; }
		}
		public string PostBody
		{
			get { return this.body; }
		}
		public string ImagePath
		{
			get { return this.imgName; }
			set { if (File.Exists(value)) this.imgName = value; }
		}
		public DateTime PostTime
		{
			get { return this.time; }
		}
		public Bitmap ImageBitmap
		{
			get
			{
				try
				{
					if (this.imgBmap == null)
						this.imgBmap = new Bitmap(this.imgName);

					return this.imgBmap;
				}
				catch (Exception e)
				{
					DebugConsole.ShowWarning("Error loading image for post ID " + this.id + ": " + e.GetType().Name + " " + e.Message);
					return null;
				}
			}
		}
		#endregion

		public Post(int id, string body, string imgPath, DateTime time)
		{
			this.id = id;
			this.body = body;
			this.imgName = imgPath;
			this.time = time;
		}

		public void PreloadBitmap()
		{
			this.ImageBitmap.ToString();
		}
	}
}
