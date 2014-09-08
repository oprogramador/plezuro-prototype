using System;
using System.Collections.Generic;using MyTypes;
using MyTypes.MyClasses;

namespace MyCollections {
	public interface IEvalable {
		object EvalDyn(ProcedureT list, IPrintable pr, TupleT args);
		object EvalDyn(ProcedureT list, IPrintable pr);
	}
}
