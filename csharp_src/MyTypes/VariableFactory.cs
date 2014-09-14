/*
 * VariableFactory.cs
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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace MyTypes {
	class VariableFactory {
		private static VariableFactory instance;
		public List<object> Constants { get; private set; }

		public static VariableFactory GetInstance() {
			if(instance==null) instance = new VariableFactory();
			return instance;
		}

		private VariableFactory() {
			Constants = new List<object>();
			string @namespace = "MyTypes.MyClasses";
			var q = from t in Assembly.GetExecutingAssembly().GetTypes()
				where t.IsClass && t.Namespace == @namespace
				select t;
			q.ToList().ForEach(t => {
					var pro = t.GetField("Constants");
					if(pro!=null) foreach(var i in (object[])pro.GetValue(null)) {
						Constants.Add(i);
					}
			});
		}
	}
}
