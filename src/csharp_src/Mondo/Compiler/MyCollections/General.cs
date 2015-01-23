/*
 * General.cs
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
 

using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
namespace Mondo.MyCollections {
	static class General {
		public static string EnumToString(IEnumerable ie, string delim=", ") {
			string str = "";
			foreach(var item in ie) str += (str=="" ? "" : delim) +item;
			return "{"+str+"}";
		}

		public static int Compare(IEnumerable a, IEnumerable b) {
			var ae = a.GetEnumerator();
			var be = b.GetEnumerator();
			var an = ae.MoveNext();
			var bn = be.MoveNext();
			while(true) {
				if(!an && bn) return -1;
				if(an && !bn) return 1;
				if(!an && !bn) return 0;
				try {
					int cmp = CompareDE(ae.Current, be.Current);
					if(cmp!=0) return cmp;
				} catch(Exception e) {
					Console.WriteLine("MyCollections.General e: "+e);
					return 0;
				}
				an = ae.MoveNext();
				bn = be.MoveNext();
			}
		}

		public static int CompareDE(object a, object b) {
			if(a is DictionaryEntry && b is DictionaryEntry) {
				var ae = (DictionaryEntry)a;
				var be = (DictionaryEntry)b;
				int cmp = 0;
				if(ae.Key is IComparable) cmp = ((IComparable)ae.Key).CompareTo(be.Key);
				else if(be.Key is IComparable) cmp = ((IComparable)be.Key).CompareTo(ae.Key);
				if(cmp!=0) return cmp;
				if(ae.Value is IComparable) cmp = ((IComparable)ae.Value).CompareTo(be.Value);
				return cmp;
			}
			if(a is IComparable) return ((IComparable)a).CompareTo(b);
			if(b is IComparable) return ((IComparable)b).CompareTo(a);
			return 0;
		}

		public static bool Converges(string str, string ch, int index) {
			for(int i=index; i<str.Length && i-index<ch.Length; i++) if(str[i] != ch[i-index]) return false;
			return true;
		}

		public static bool Converges(StringBuilder str, string ch, int index) {
			for(int i=index; i<str.Length && i-index<ch.Length; i++) if(str[i] != ch[i-index]) return false;
			return true;
		}

		public static object[] Shift(object[] ar, object x) {
			object[] ret = new object[ar.Length+1];
			ret[0] = x;
			for(int i=0; i<ar.Length; i++) ret[i+1] = ar[i];
			return ret;
		}
	}
}
