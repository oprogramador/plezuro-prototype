/*
 * BuiltinFunc.cs
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
	class BuiltinFunc : IVariable, ICallable  {
		public Func<IPrintable,object[],ReferenceT> Proc { get; private set; }

		public BuiltinFunc(Func<IPrintable,object[],ReferenceT> p, int argnr, bool stat, bool vararg) {
			ID = ObjectContainer.Instance.Add(this);
			Proc = p;
			Argnr = argnr;
			IsStatic = stat;
			IsVarArg = vararg;
		}

		public int Argnr { get; private set; }
		public bool IsStatic { get; private set; }
		public bool IsVarArg { get; private set; }

		public object Call(IPrintable p, object[] argss) {
			return Proc(p,argss);
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
			return 0;
		}

		public object Clone() {
			return new BuiltinFunc(Proc, Argnr, IsStatic, IsVarArg);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		private static ClassT myClass;
		
		public const string ClassName = "BuiltinFunc";

		private static object[] lambdas = {
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
						typeof(BuiltinFunc)
					);
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "builtin: "+Proc;
		}
	}
}
