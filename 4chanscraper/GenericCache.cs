using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scraper
{
	public class GenericCache<K, V> : IEnumerable<V> 
		where K : IComparable<K>
	{
		private int maxImages = 10;
		private Dictionary<K, V> cacheStore = null;
		private Queue<K> arrivalOrder = null;

		public int MaxImages
		{
			get { return this.maxImages; }
			set { this.maxImages = value; }
		}
		public int Count
		{
			get { return (cacheStore == null) ? 0 : cacheStore.Count; }
		}
		public V this[K k] { get { return this.GetValue(k); } }

		#region Constructor
		public GenericCache() { }
		public GenericCache(int maxImages)
		{
			this.maxImages = maxImages;
		}
		#endregion

		#region Public Methods
		public void Add(K k, V v)
		{
			if (this.ContainsKey(k))
				this.Remove(k);

			if (this.maxImages + 1 > this.maxImages)
				this.purgeSpace();

			this.storeItem(k, v);
		}
		public void Remove(K k)
		{
			if (this.ContainsKey(k))
			{
				this.removeKeyFromQueue(k);
				this.cacheStore.Remove(k);
			}
		}
		public void PurgeAll()
		{
			this.arrivalOrder.Clear();
			this.cacheStore.Clear();
		}

		public void Touch(K k)
		{
			this.removeKeyFromQueue(k);
			this.arrivalOrder.Enqueue(k);
		}
		public V GetValue(K k)
		{
			if (this.cacheStore != null && this.cacheStore.ContainsKey(k))
			{
				this.Touch(k);
				return this.cacheStore[k];
			}
			else
				return default(V);
		}

		public bool ContainsKey(K k)
		{
			return (this.cacheStore != null && this.cacheStore.ContainsKey(k));
		}

		public IEnumerable<K> GetKeys()
		{
			foreach (K k in this.arrivalOrder)
				yield return k;
		}
		public IEnumerable<V> GetValues()
		{
			foreach (KeyValuePair<K, V> i in this.cacheStore)
				yield return i.Value;
		}
		public IEnumerable<KeyValuePair<K, V>> GetItems()
		{
			foreach (KeyValuePair<K, V> i in this.cacheStore)
				yield return i;
		}

		public IEnumerator<V> GetEnumerator()
		{
			foreach (KeyValuePair<K, V> i in this.cacheStore)
				yield return i.Value;
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		#endregion

		#region Private Methods
		private void purgeSpace()
		{
			if (this.cacheStore != null && this.arrivalOrder != null && this.cacheStore.Count != 0)
			{
				if(typeof(IDisposable).IsAssignableFrom(typeof(V)))
					(this.cacheStore[this.arrivalOrder.Peek()] as IDisposable).Dispose();
				this.cacheStore.Remove(this.arrivalOrder.Dequeue());
			}
		}
		private void removeKeyFromQueue(K k)
		{
			if (this.arrivalOrder.Contains(k))
			{
				if (this.arrivalOrder.Peek().CompareTo(k) == 0)
					this.arrivalOrder.Dequeue();
				else
				{
					Queue<K> tempQueue = new Queue<K>();
					int oldQueueSize = this.arrivalOrder.Count;
					while (this.arrivalOrder.Count > 0)
					{
						K tempValue = this.arrivalOrder.Dequeue();

						if (tempValue.CompareTo(k) != 0)
							tempQueue.Enqueue(tempValue);
					}
					this.arrivalOrder = tempQueue;
				}
			}
		}

		private void storeItem(K k, V v)
		{
			if (this.cacheStore == null)
			{
				// Create the stores.
				this.cacheStore = new Dictionary<K, V>();
				this.arrivalOrder = new Queue<K>();
			}
			this.arrivalOrder.Enqueue(k);
			this.cacheStore.Add(k, v);
		}
		#endregion	
	}
}
