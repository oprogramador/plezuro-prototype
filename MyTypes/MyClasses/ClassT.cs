using System;
using System.Collections.Generic;

namespace MyTypes.MyClasses {
	public class ClassT : IItem, IVariable {
		public List<ClassT> Parents{ get; private set; }
		private Dictionary<string,Method> methods;
		public string Name{ get; private set; }
		public PackageT Package{ get; private set; }

		public ClassT(string name, List<ClassT> parents, Dictionary<string,Method> meth, PackageT package) {
			ID = ObjectContainer.Instance.Add(this);
			Name = name;
			Parents = parents;
			methods = meth;
			Package = package;
		}

		public static ClassT GetClass(object [] args) {
			return args.Length>0 ? ((IVariable)args[0]).GetClass() : EmptyT.MyClass;
		}

		public override string ToString() {
			return "class "+Name;
		}

		public Method GetMethod(string name) {
			try {
				return methods[name];
			} catch {
				foreach(var p in Parents) {
					try {
						return p.GetMethod(name);
					} catch {
						continue;
					}
				}
			}
			throw new NoMethodException();
		}

		public int ID { get; private set; }

		public int CompareTo(object ob) {
			return 0;
		}

		public object Clone() {
			return new ClassT(Name,Parents,methods,Package);
		}

		IVariable[] ITuplable.ToArray() {
			return new IVariable[]{this};
		}

		public static readonly ClassT MyClass;
		private static readonly Dictionary<string,Method> myMethods;

		private static object[] lambdas = {
		};
		
		static ClassT() {
			myMethods = LambdaConverter.Convert( lambdas );
 			MyClass = new BuiltinClass( "Class", new List<ClassT>(){ObjectT.MyClass}, myMethods, PackageT.Lang, typeof(ClassT) ); 
		}

		public ClassT GetClass() {
			return MyClass;
		}

		public object ObValue {
			get { return this; }
			set { }
		}
	}
}
