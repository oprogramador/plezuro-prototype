/*
 * IfObject.cs
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
 
using Mondo.MyCollections;

using System;
using System.Collections.Generic;

namespace Mondo.MyTypes.MyClasses {
	class IfObject : IVariable {
		public bool Bool { get; private set; }
		public IVariable Res { get; private set; }
		
		public IfObject(bool b, IVariable r) {
			Bool = b;
			Res = r;
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
			if(ob is IfObject) {
				pre = Bool.CompareTo(((IfObject)ob).Bool);
				if(pre!=0) return pre;
				pre = Res.CompareTo(((IfObject)ob).Res);
				if(pre!=0) return pre;
			}
			return 0;
		}

		public object Clone() {
			return new IfObject(Bool, Res);
		}

		public override string ToString() {
			return "IfObject("+Bool+","+Res+")";
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public const string ClassName = "IfObject";

		private static ClassT myClass;

		private static object[] lambdas = {
			"elif",	(Func<IPrintable,IfObject,ProcedureT,ProcedureT,IfObject>) 
				((p, io, con, o) => { 
				 	if(io.Bool) return io;
				 	IVariable res = new NullType(); 
				 	bool b = p.EvalDyn(con).Equals(true); 
				 	if(b) res = (IVariable)p.EvalDyn(o); 
				 	return new IfObject(b,res); 
				 }),
			"if",	(Func<IPrintable,IfObject,ProcedureT,ProcedureT,IfObject>) 
				((p, io, con, o) => { 
				 	if(!io.Bool) return io;
				 	IVariable res = new NullType(); 
				 	bool b = p.EvalDyn(con).Equals(true); 
				 	if(b) res = (IVariable)p.EvalDyn(o); 
				 	return new IfObject(b,res); 
				 }),
			"else",	(Func<IPrintable,IfObject,ProcedureT,IfObject>) 
				((p, io, o) => { 
				 	if(io.Bool) return io;
				 	var res = (IVariable)p.EvalDyn(o); 
				 	return new IfObject(true,res); 
				 }),
			"bool",		(Func<IfObject,bool>) ((x) => x.Bool),
			"res",		(Func<IfObject,IVariable>) ((x) => x.Res),
		};
		
		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(IfObject) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
