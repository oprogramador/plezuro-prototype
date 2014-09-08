using System;
using System.Collections.Generic;using MyTypes;
using MyTypes.MyClasses;
using MyCollections;

namespace MyCollections {
	public interface IPrintable : IEvalable, IVariable {
		void Print(object ob);
		void PrintLine(object ob);
		IPrintable Parent{ get; }
		ITextable Stream{ get; }
		TupleT Args{ get; }
		bool Clear();
		ConcurrentDictionary<string,object> LocalVars{ get; }
	}
}
