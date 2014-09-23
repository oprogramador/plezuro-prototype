/*
 * WList.cs
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
namespace Mondo.MyCollections {
	public class WList<T> : List<T> {
		public override string ToString() {
			return General.EnumToString(this);
		}
		
		public T Peek() {
			return this[Count-1];
		}

		public void Shift(T ic) {
			Insert(0,ic);
		}

		public T Pop() {
			T v = this[Count-1];
			RemoveAt(Count-1);
			return v;
		}
		
		public WList<T> Concat(WList<T> st, Type t) {
			var ret = (WList<T>)Activator.CreateInstance(t,this);
			ret.AddRange(st);
			return ret;
		}

	}
}
