using System;
using System.Collections.Generic;
using MyCollections;

namespace MyTypes {
	public interface ICallable : IComparable {
		object Call(IPrintable p, object[] args);
	}
}
