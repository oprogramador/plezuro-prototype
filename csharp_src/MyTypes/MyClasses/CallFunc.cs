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
using MyCollections;

namespace MyTypes.MyClasses {
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
			Console.WriteLine("tomytype ob ="+TypeTrans.toMyType(ob)+" type="+TypeTrans.toMyType(ob));
			Args = TupleT.Add( Args, TypeTrans.toMyType(ob) );
			return this;
		}

		public object Call(IPrintable p) {
			return Proc.Call(p,Args.ToArray());
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			int pre = TypeT.PreCompare(this,ob);
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

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;


		private static object[] lambdas = {

		};
		
		static CallFunc() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "CallFunc", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(CallFunc) ); 
		}

		public ClassT GetClass() {
			return MyClass;
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
