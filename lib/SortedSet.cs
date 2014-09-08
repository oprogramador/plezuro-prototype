using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic {
	class SortedSet<T> : SortedDictionary<T,int> {
		public void Add(T x) {
			base.Add(x,0);
		}

		public T Max() {
			return base.Keys.Max();
		}

		public T Min() {
			return base.Keys.Min();
		}

		public virtual bool Contains(T x) {
			return base.ContainsKey(x);
		}

		public SortedSet<T> IntersectWith(SortedSet<T> x) {
			var s = new SortedSet<T>();
			foreach(T i in x) if(Contains(i)) s.Add(i);
			return s;
		}
		
		public new IEnumerator GetEnumerator() {
			using(var ie = base.GetEnumerator())
				while(ie.MoveNext()) {
					yield return ie.Current.Key;
				}
		}
	}
}
