/*
 * SList.cs
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
using System;
using System.Collections.Generic;using MyTypes;
using MyTypes.MyClasses;

namespace MyCollections {
	public class SList<T> : CList<T>, ICompCloneable where T : ICompCloneable {
		
		public SList() : base() {

		}

		public SList(IEnumerable ie) {
			foreach(var item in ie)  {
				Add((T)(item is ICompCloneable ? ((ICompCloneable)item).Clone() : Variable.Convert(item)));
			}
		}
		
		public virtual object Clone() {
			return new SList<T>(this);
		}

		public static implicit operator SList<T>(List<object> l) {
			return new SList<T>(l);
		}

		public SList<T> Concat(SList<T> st) {
			var ret = new SList<T>(this);
			ret.AddRange(st);
			return ret;
		}

	}
}
