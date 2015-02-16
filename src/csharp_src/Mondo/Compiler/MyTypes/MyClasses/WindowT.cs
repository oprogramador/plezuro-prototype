/*
 * WindowT.cs
 * Copyright 2015 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
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
using System.Windows.Forms;
using System.Collections.Generic;
using Mondo.Engine;

namespace Mondo.MyTypes.MyClasses {
	class WindowT : Form, IVariable {
		public int ID { get; private set; }
                public DictionaryT Dictionary { get; private set; }

		public WindowT(DictionaryT dic) {
			ID = ObjectContainer.Instance.Add(this);
                        Width = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("w"))]).Value).Value;
                        Height = (int)((Number)((ReferenceT)dic[new ReferenceT(new StringT("h"))]).Value).Value;
                        Console.WriteLine("dic="+dic+" w="+Width+" h="+Height);
                        Show();
                        Dictionary = dic;
		}

		public override bool Equals(object ob) {
			return CompareTo(ob)==0;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public int CompareTo(object ob) {
			int pre = ReferenceT.PreCompare(this,ob);
			if(pre!=0) return pre;
			return 0;
		}

		public object Clone() {
			return new WindowT((DictionaryT)Dictionary.Clone());
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{};
		}

		private static ClassT myClass;

		public const string ClassName = "Window";

		private static object[] lambdas = {
                        "close",	(Func<WindowT,bool>) ((x) => {x.Close(); return true;}),
		};

		public ClassT GetClass() {
			return StaticGetClass();
		}

		public static ClassT StaticGetClass() {
			if(myClass==null) myClass = 
				new BuiltinClass( ClassName, new List<ClassT>(){ObjectT.StaticGetClass()}, LambdaConverter.Convert(lambdas), PackageT.Lang, typeof(WindowT) );
			return myClass;
		}	

		public object ObValue {
			get { return this; }
			set { }
		}

		public override string ToString() {
			return "window()";
		}

	}
}
