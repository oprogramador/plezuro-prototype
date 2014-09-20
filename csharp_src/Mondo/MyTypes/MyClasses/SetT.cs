/*
 * SetT.cs
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
using System.Linq;
using System.Collections.Generic;
using Mondo.MyCollections;

namespace Mondo.MyTypes.MyClasses {
	class SetT : SortedSet<IVariable>, IVariable {
		public SetT() :  base() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public SetT(IEnumerable ie) :  base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(var i in ie)
			try {
				Add((IVariable)i);
			} catch{}
		}

		public SetT(SetT s) : base() {
			ID = ObjectContainer.Instance.Add(this);
			foreach(IVariable i in s)
			try {
				Add(i);
			} catch{}
		}

		public override bool Contains(IVariable x) {
			return base.Contains(new ReferenceT(x));
		}

		public SetT IntersectWith(SetT x) {
			var s = new SetT();
			foreach(IVariable i in x) if(base.Contains(i)) s.Add(i);
			return s;
		}

		public SetT UnionWith(SetT x) {
			var s = new SetT(this);
			Console.WriteLine("set="+s);
			foreach(IVariable i in x)
			try {
				s.Add(i);
			} catch{}
			return s;
		}

		public SetT ExceptWithSelf(SetT x) {
			var s = new SetT();
			foreach(IVariable i in x) if(!base.Contains(i)) s.Add(i);
			return s;
		}

		public SetT ExceptWith(SetT x) {
			return x.ExceptWithSelf(this);
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return General.Compare(this, (IEnumerable)ob); 
		} 

		public object Clone() {
			return new SetT(this);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT MyClass;

		public static object[] Constants = {
			"db",	DataFixtures.DataFixtures.GetInstance()
		};

		private static object[] lambdas = {
			"len",		(Func<SetT,double>) ((a) => a.Count),
			"max",		(Func<SetT,IVariable>) ((x) => (IVariable)((ReferenceT)x.Max()).Value),
			"min",		(Func<SetT,IVariable>) ((x) => (IVariable)((ReferenceT)x.Min()).Value),
			"contains",	(Func<SetT,IVariable,bool>) ((a,x) => a.Contains(x)),
			"join",		(Func<SetT,SetT,SetT>) ((x,y) => x.IntersectWith(y)),
			"except",	(Func<SetT,SetT,SetT>) ((x,y) => x.ExceptWith(y)),
			"union",	(Func<SetT,SetT,SetT>) ((x,y) => x.UnionWith(y)),
			"remove",	(Func<SetT,IVariable,SetT>) ((x,i) => {var y=(SetT)x.Clone(); y.Remove(new ReferenceT(i)); return y;}),
			"toList",	(Func<SetT,IVariable>) ((x) => new ListT(x.Keys.ToList())),
			"<<",		(Func<SetT,IVariable,SetT>) ((a,v) => {a.Add(new ReferenceT((IVariable)v.Clone())); return a;} ),
		};
		
		public virtual ClassT GetClass() {
			if(MyClass==null) MyClass = 
				new BuiltinClass( "Set", 
						new List<ClassT>(){ObjectT.StaticGetClass()},
						LambdaConverter.Convert(lambdas),
						PackageT.Lang,
						typeof(SetT) );
			return MyClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			string ret = "{";
			foreach(var i in this) ret += (ret=="{" ? "" : ",")+i;
			ret += "}";
			return ret;
		}
	}
}
