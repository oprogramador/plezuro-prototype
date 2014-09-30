/*
 * MyClass.cs
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
using Mondo.MyCollections;

namespace Mondo.MyTypes.MyClasses {
	class MyClass : ClassT {
		public MyClass(string name, ListT parents, DictionaryT methods, PackageT package)
		     : base(name, toList(parents), toDic(methods), package) {
		     }		     

		private static List<ClassT> toList(ListT l) {
			var ret = new List<ClassT>();
			foreach(var i in l) ret.Add((ClassT)TypeTrans.dereference(i));
			return ret;
		}

		private static Dictionary<string,Method> toDic(DictionaryT dic) {
			var ret = new Dictionary<string,Method>();
			foreach(var i in dic) {
				var method = TypeTrans.dereference(i.Value);
				var finalMethod = method is Method ? (Method)method : new Method((ProcedureT)method, new AccessModifier(AccessEnum.Public));
				ret.Add(((StringT)TypeTrans.dereference(i.Key)).Value, finalMethod);
			}
			return ret;
		}
	}
}
