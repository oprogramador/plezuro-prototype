/*
 * GeneralIndexer.cs
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
using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.MyCollections {
	static class GeneralIndexer {
		private static IVariable numIndex(IIndexable ii, int i) {
			return (IVariable)(i>=0 ? ii.At(i) : ii.At(ii.Count+i));
		}

		private static object[] pairIndex(IIndexable ii, int beg, int end) {
			var list = new ListT();
			if(beg<end) for(int i=beg; i<end; i++) {
				list.Add(numIndex(ii, i));
			}
			return list.ToArray();
		}

		private static object[] rangeIndex(IIndexable ii, RangeT r) {
			var list = new ListT();
			foreach(var i in r) list.Add(numIndex(ii, (int)((Number)i).Value));
			return list.ToArray();
		}

		public static object ivarIndex(IIndexable ii, IVariable iv) {
			var i = TypeTrans.dereference(iv);
			if(i is Number) return numIndex(ii, (int)((Number)i).Value);
			if(i is PairT) return pairIndex(ii, (int)((Number)((PairT)i).Key).Value, (int)((Number)((PairT)i).Value).Value);
			if(i is RangeT) return rangeIndex(ii, (RangeT)i);
			return null;
		}

		public static object[] listIndex(IIndexable ii, ListT lt) {
			var list = new List<object>();
			foreach(var i in lt) {
				var ivi = ivarIndex(ii, (IVariable)i);
				if(ivi is object[]) list.AddRange((object[])ivi);
				else list.Add(ivi);
			}
			return list.ToArray();
		}

		public static ITuplable Index(IIndexable ii, IVariable iv) {
			var i = TypeTrans.dereference(iv);
			if(i is ListT) return TupleT.MakeTuplable(listIndex(ii, (ListT)i));
			return TupleT.MakeTuplable(ivarIndex(ii, (IVariable)i));
		}
	}
}
