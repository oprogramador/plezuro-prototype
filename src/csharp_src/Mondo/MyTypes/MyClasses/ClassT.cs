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
using System.Collections;
using Mondo.MyCollections;

namespace Mondo.MyTypes.MyClasses {
	public class ClassT : IItem, IVariable, ICallable {
		public ListT Parents { get; private set; }
		public DictionaryT Methods { get; private set; }
		public string Name{ get; private set; }
		public PackageT Package{ get; private set; }

		public ClassT(string name, List<ClassT> parents, Dictionary<string,Method> meth, PackageT package) {
			ID = ObjectContainer.Instance.Add(this);
			Name = name;
			Parents = new ListT(parents);
			Methods = new DictionaryT(meth);
			Package = package;
			try {
				package.Items.Add(new ReferenceT(new StringT(name)), new ReferenceT(this));
			} catch {}
		}

		public ClassT(string name, ListT parents, DictionaryT meth, PackageT package) {
			ID = ObjectContainer.Instance.Add(this);
			Name = name;
			Parents = parents;
			Methods = meth;
			Package = package;
			try {
				package.Items.Add(new StringT(name), this);
			} catch {}
}

		public object Call(IPrintable p, object[] argss) {
			var ob = (this is BuiltinClass ? Activator.CreateInstance(((BuiltinClass)this).Type) : new MyObject(this));
			try {
				var f = GetMethod("init");
				var ar = new object[argss.Length+1];
				ar[0] = ob;
				argss.CopyTo(ar,1);
				f.Call(p,ar);
			} catch {}
			return ob;
		}

		public static ClassT GetClass(object [] args) {
			return args.Length>0 ? ((IVariable)args[0]).GetClass() : EmptyT.StaticGetClass();
		}

		public override string ToString() {
			return "class "+Name;
		}

		public Method GetMethod(string name) {
			try {
				return (Method)TypeTrans.dereference(Methods[new ReferenceT(new StringT(name))]);
			} catch {
				foreach(var p in Parents) {
					try {
						var pp = (ClassT)TypeTrans.dereference(p);
						return pp.GetMethod(name);
					} catch {
						continue;
					}
				}
			}
			throw new NoMethodException();
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is ClassT) return Name.CompareTo(((ClassT)ob).Name);
			return 0;
		}

		public object Clone() {
			return new ClassT(Name,Parents,Methods,Package);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT myClass;
		public const string ClassName = "Class";

		protected static object[] lambdas = {
			"methods",	(Func<ClassT,DictionaryT>) ((c) => c.Methods),
			"parents",	(Func<ClassT,ListT>) ((c) => c.Parents),
			"package",	(Func<ClassT,PackageT>) ((c) => c.Package),
			"new",		(Func<ClassT,object>) 
					((c) => c is BuiltinClass ? Activator.CreateInstance(((BuiltinClass)c).Type) : new MyObject(c)),
			"@",		(Func<ClassT,DictionaryT>) ((c) => new DictionaryT(c.Methods)),
		};

		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass(
						ClassName,
						new List<ClassT>(){Callable.StaticGetClass()},
						LambdaConverter.Convert(lambdas),
						PackageT.Lang,
						typeof(ClassT)
					);
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
