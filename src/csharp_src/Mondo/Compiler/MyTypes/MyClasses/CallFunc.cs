/*
 * CallFunc.cs
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
	class CallFunc : IVariable {
		public ITuplable Args { 
			get; 
			private set; 
		}

		public ICallable Proc { get; private set; }

		public CallFunc(ICallable p) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
		}

		public CallFunc(ICallable p, ITuplable args) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			Args = args;
		}

		public CallFunc Push(object ob) {
			Args = TupleT.Add( Args, TypeTrans.toMyType(ob) );
			return this;
		}

		public object Call(IPrintable p) {
			return Proc.Call(p,Args.ToArray());
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
			if(ob is CallFunc) Proc.CompareTo(((CallFunc)ob).Proc);
			return 0;
		}

		public object Clone() {
			return new CallFunc(Proc);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "CallFunc";

		private static object[] lambdas = {

		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(CallFunc) );
			return myClass;
		}	

		public object ObValue {
			get { return Proc; }
			set { }
		}

		public override string ToString() {
			return "(callfunc: "+Proc+"args: "+Args+")";
		}
	}
}
