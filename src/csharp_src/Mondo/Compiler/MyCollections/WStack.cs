/*
 * WStack.cs
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
using System.Collections.Generic;using Mondo.MyTypes;
using Mondo.MyTypes.MyClasses;

namespace Mondo.MyCollections {
	public class WStack<T> : Stack<T>, IVariable {
		public const string ClassName = "WStack";
		private static ClassT myClass;

		private static object[] lambdas = {

		};
	
		public virtual ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(TupleT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return General.EnumToString(this);
		}

		public WStack() : base() {
			ID = ObjectContainer.Instance.Add(this);
		}

		public WStack(WStack<T> ws) :  base(ws) {
			ID = ObjectContainer.Instance.Add(this);
		}

		public WStack<T> Reverse() {
			var ws = new WStack<T>();
			foreach(var i in this) ws.Push(i);
			return ws;
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
			if(ob is WStack<T>) return new SList<ICompCloneable>(this).CompareTo(new SList<ICompCloneable>((WStack<T>)ob));
			return 0;
		}

		public virtual object Clone() {
			return new WStack<T>(this);
		}

		public T NthElement(int n) {
			var s = new Stack<T>();
			for(int i=0; i<n; i++) s.Push(Pop());
			var ret = Peek();
			for(int i=0; i<n; i++) Push(s.Pop());
			return ret;
		}

		public void Insert(int n, T x) {
			var s = new Stack<T>();
			for(int i=0; i<n; i++) s.Push(Pop());
			Push(x);
			for(int i=0; i<n; i++) Push(s.Pop());
		}
		
		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		/*public string TypeName() {
			return "stack";
		}*/
	}
}
