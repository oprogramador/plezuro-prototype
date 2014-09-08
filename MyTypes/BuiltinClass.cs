using System;
using System.Collections.Generic;
using MyTypes.MyClasses;

namespace MyTypes {
	class BuiltinClass : ClassT {
		public Type Type{ get; private set; }

		public BuiltinClass(string name, List<ClassT> parents, Dictionary<string,Method> methods, PackageT package, Type type) 
		: base(name,parents,methods,package) {
			Type = type;
		}	

		public override string ToString() {
			return "builtinClass "+Name;
		}
	}
}
