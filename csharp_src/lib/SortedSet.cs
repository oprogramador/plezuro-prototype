/*
 * SortedSet.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

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
