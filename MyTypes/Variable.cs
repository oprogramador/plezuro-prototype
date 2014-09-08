using System;
using System.Collections.Generic;
using MyTypes.MyClasses;

namespace MyTypes {
	static class Variable {
		public static IVariable Convert(object ob) {
			if(ob is double) return new Number((double)ob);
			if(ob is int) return new Number((int)ob);
			if(ob is string) return new StringT((string)ob);
			if(!(ob is IVariable)) {Console.WriteLine("exc type:"+ob.GetType()); throw new NotComparableException();}
			return (IVariable)ob;
		}
	}
}
