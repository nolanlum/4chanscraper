using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper.Data
{
	[Serializable]
	public class Thread
	{
		private int id;
		private string name;
		private List<Post> posts;

		[NonSerialized]
		private bool isNew;

		public int Id
		{
			get { return this.id; }
		}
		public Post this[int index]
		{
			get { return this.posts[index]; }
		}
		public int Count
		{
			get { return this.posts.Count; }
		}
		public string Name
		{
			get { if (this.name == null) return this.id.ToString(); else  return this.name; }
			set { this.name = value; }
		}
		public bool FullyDownloaded
		{
			get { foreach (Post p in this.posts) if (p.ImagePath.Contains("http:")) return false; return true; }
		}
		public bool IsNewThread
		{
			get { return this.isNew; }
			set { this.isNew = value; }
		}
		public bool HasNewPosts
		{
			get { foreach (Post p in this.posts) if (p.IsNewPost) return true; return false; }
		}

		public Thread(int id, string name)
		{
			this.id = id;
			this.name = name;
			this.posts = new List<Post>();
		}
		public Thread(int id) : this(id, null) { }

		public void AddPost(Post post)
		{
			post.IsNewPost = true;
			this.posts.Add(post);
		}
		public void RemovePost(Post p)
		{
			this.posts.Remove(p);
		}

		public IEnumerator<Post> GetEnumerator()
		{
			return this.posts.GetEnumerator();
		}

		public static Thread operator +(Thread t1, Thread t2)
		{
			if (t1.id != t2.id) throw new InvalidOperationException("You cannot merge different threads.");

			if (t1.Count == 0 && t2.Count > 0) return t2; // T1 is empty.
			if (t1.Count > 0 && t2.Count == 0) return t1; // T2 is empty.

			Thread newT = new Thread(t1.id);
			newT.name = t1.name;
			// Even though posts don't re-order on 4chan, you can never be too careful.
			for (int i = 0, j = 0; i < t1.Count || j < t2.Count; )
			{
				if (i == t1.Count) newT.AddPost(t2[j++]);
				else if (j == t2.Count) newT.AddPost(t1[i++]);
				else
					if (t1[i] < t2[j]) // T1's post comes before (o.o) T2's post, merge in T1.
						newT.AddPost(t1[i++]);
					else if (t1[i] > t2[j]) // T2's post comes before (o.o) T1's post, merge in T2.
						newT.AddPost(t2[j++]);
					else // IDs match, check downloaded state.
						if (t1[i].ImagePath == t2[j].ImagePath)
						{
							newT.AddPost(t1[i]); t1[i++].IsNewPost = false; j++;
						}
						else if (t1[i].ImagePath.Contains("http:"))
						{
							newT.AddPost(t2[j]); t2[j++].IsNewPost = false; i++;
						}
						else
						{
							newT.AddPost(t1[i]); t1[i++].IsNewPost = false; j++;
						}
			}

			return newT;
		}
	}
}
