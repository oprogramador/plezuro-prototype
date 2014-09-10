/*
 * HtmlDicTable.cs
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
using System.Collections.Generic;
using System.Linq;

namespace lib {
	class HtmlDicTable : HtmlTable {
		private HashSet<object> properties;

		public HtmlDicTable(IEnumerable ar) : base(ar) {
			properties = new HashSet<object>();
			foreach(var i in ar) {
				//if(i is IDictionary) properties.UnionWith( ((IDictionary)i).Keys );
				try {
					foreach(var k in ((IDictionary)SimpleTypeConverter.Convert(i)).Keys) if(!properties.Contains(k)) properties.Add(k);
				} catch {}
			}
		}

		public override string Generate() {
			string ret = "<table border=\""+Border+"\">";
			ret += "<tr>";
			foreach(var p in properties) ret += "<td>"+SimpleTypeConverter.Convert(p)+"</td>";
			ret += "</tr>";
			foreach(var rowi in array) {
				ret += "<tr>";
				var row = SimpleTypeConverter.Convert(rowi);
				if(row is IDictionary) foreach(var p in properties) {
					object cell = "null";
					try {
						cell = ((IDictionary)row)[p];
					} catch {}
					ret += "<td>"+SimpleTypeConverter.Convert(cell)+"</td>";
				}
				else ret += "<td>"+row+"</td>";
				ret += "</tr>";
			}
			ret += "</table>";
			return ret;
		}
	}
}
