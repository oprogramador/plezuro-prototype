/*
 * ClassT.cs
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

namespace MyTypes.MyClasses {
	public class ClassT : IItem, IVariable {
		public List<ClassT> Parents{ get; private set; }
		private Dictionary<string,Method> methods;
		public string Name{ get; private set; }
		public PackageT Package{ get; private set; }

		public ClassT(string name, List<ClassT> parents, Dictionary<string,Method> meth, PackageT package) {
			ID = ObjectContainer.Instance.Add(this);
			Name = name;
			Parents = parents;
			methods = meth;
			Package = package;
		}

		public static ClassT GetClass(object [] args) {
			return args.Length>0 ? ((IVariable)args[0]).GetClass() : EmptyT.MyClass;
		}

		public override string ToString() {
			return "class "+Name;
		}

		public Method GetMethod(string name) {
			try {
				return methods[name];
			} catch {
				foreach(var p in Parents) {
					try {
						return p.GetMethod(name);
					} catch {
						continue;
					}
				}
			}
			throw new NoMethodException();
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			return 0;
		}

		public object Clone() {
			return new ClassT(Name,Parents,methods,Package);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT MyClass;
		//private static readonly Dictionary<string,Method> myMethods;

		protected static object[] lambdas = {
			"parents",	(Func<ClassT,ListT>) ((c) => new ListT(c.Parents)),
		};
		
		public virtual ClassT GetClass() {
			if(MyClass==null) MyClass = 
				new BuiltinClass( "Class", new List<ClassT>(){ObjectT.MyClass}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(ClassT) );
			return MyClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
