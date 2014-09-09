/*
 * HtmlTable.cs
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
using System.Collections;

namespace lib {
	class HtmlTable {
		private IEnumerable array;
	
		private int border;

		public int Border { get {return border;} set {}}
		public HtmlTable SetBorder(int b) {
			border = b;
			return this;
		}

		public HtmlTable(IEnumerable ar) {
			border = 0;
			array = ar;
		}

		public string Generate() {
			string ret = "<table border=\""+border+"\">";
			foreach(var rowi in array) {
				ret += "<tr>";
				var row = SimpleTypeConverter.Convert(rowi);
				if(row is IEnumerable) foreach(var cell in (IEnumerable)row) ret += "<td>"+SimpleTypeConverter.Convert(cell)+"</td>";
				else ret += "<td>"+row+"</td>";
				ret += "</tr>";
			}
			ret += "</table>";
			return ret;
		}
	}
}
