/*
 * PackageT.cs
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

namespace Mondo.MyTypes.MyClasses {
	public class PackageT : List<IItem>, IItem, IVariable {
		public string Name{ get; private set; }
		public PackageT Package{ get; private set; }

		public PackageT(string name, PackageT package) {
			Name = name;
			Package = package;
		}

		public static PackageT Lang = new PackageT("Lang", null);

		public override string ToString() {
			return "package "+Name;
		}

		public int ID { get; private set; }

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			//int pre = ClassT.PreCompare(this,ob);
			//if(pre!=0) return pre;
			//if(ob is PackageT) return Name.CompareTo(((PackageT)ob).Name);
			return 0;
		}

		public object Clone() {
			return new PackageT(Name, Package);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static ClassT MyClass;

		protected static object[] lambdas = {
			"package",	(Func<PackageT,IVariable>) ((c) => (c.Package!=null ? (IVariable)c.Package : (IVariable)new NullType())),
		};

		public virtual ClassT GetClass() {
			if(MyClass==null) MyClass = 
				new BuiltinClass( "Package", new List<ClassT>(){ObjectT.MyClass}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(PackageT) );
			return MyClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
