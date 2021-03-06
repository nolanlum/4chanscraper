﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Scraper.Data
{
	[Serializable]
	public class Post
	{
		#region Private Members
		private int id;
		private string body;
		private string imgName;
		private DateTime time;

		[NonSerialized]
		private bool isNew = false;
		[NonSerialized]
		private string imgNameT = "";
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
			set { this.imgName = value; }
		}
		public DateTime PostTime
		{
			get { return this.time; }
		}

		public bool IsNewPost
		{
			get { return this.isNew; }
			set { this.isNew = value; }
		}
		#endregion

		public Post(int id, string body, string imgPath, DateTime time)
		{
			this.id = id;
			this.body = body;
			this.imgName = imgPath;
			this.time = time;
		}

		[OnSerializing]
		private void _beforeSerialize(StreamingContext c)
		{
			if (!this.imgName.Contains("http:"))
			{
				this.imgNameT = this.imgName;
				this.imgName = Path.GetFileName(this.imgName);
			}
		}
		[OnSerialized]
		private void _afterSerialize(StreamingContext c)
		{
			if (!this.imgName.Contains("http:"))
				this.imgName = this.imgNameT;
		}

		#region Operator Overloads and Etc
		public static bool operator ==(Post p1, Post p2)
		{
			if (Object.ReferenceEquals(p1, p2)) return true;
			if (((object) p1 == null) || ((object) p2 == null)) return false;

			return p1.id == p2.id;
		}
		public static bool operator !=(Post p1, Post p2)
		{
			return !(p1.id == p2.id);
		}
		public static bool operator <(Post p1, Post p2)
		{
			return p1.id < p2.id;
		}
		public static bool operator >(Post p1, Post p2)
		{
			return p1.id > p2.id;
		}
		public static bool operator <=(Post p1, Post p2)
		{
			return p1.id <= p2.id;
		}
		public static bool operator >=(Post p1, Post p2)
		{
			return p1.id >= p2.id;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Post)) return false;
			return this.id == ((Post) obj).id && this.imgName == ((Post) obj).imgName && this.time == ((Post) obj).time;
		}
		public override int GetHashCode()
		{
			return this.id.GetHashCode();
		} 
		#endregion
	}
}
