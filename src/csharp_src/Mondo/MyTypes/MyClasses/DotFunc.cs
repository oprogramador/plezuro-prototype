/*
 * DotFunc.cs
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
using Mondo.lib;

namespace Mondo.MyTypes.MyClasses {
	class DotFunc : IVariable, ICallable  {
		public IVariable FirstArg { 
			get;
			private set; 
		}

		public ICallable Proc { get; private set; }

		public DotFunc(ICallable p,IVariable arg) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			FirstArg = arg;
		}

		public DotFunc(ICallable p) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
		}

		public object Call(IPrintable p, object[] argss) {
			return Proc.Call( p, General.Shift(argss, FirstArg) ); 
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ClassT.PreCompare(this,ob);
			if(pre!=0) return pre;
			if(ob is DotFunc) Proc.CompareTo(((DotFunc)ob).Proc);
			return 0;
		}

		public object Clone() {
			return new DotFunc(Proc);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "DotFunc";

		private static object[] lambdas = {
			ObjectT.FunctionSymbol,	(Func<IPrintable,DotFunc,ITuplable,object>) 
				((p,f,a) => f.Call(p, a.ToArray())),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(DotFunc) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "dotFunc: "+Proc+(FirstArg==null ? "" : " firstArg: "+FirstArg);
		}
	}
}
