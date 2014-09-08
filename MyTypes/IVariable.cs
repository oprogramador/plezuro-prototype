using System;
using System.Collections.Generic;
using MyCollections;
using MyTypes.MyClasses;

namespace MyTypes {
	public interface IVariable : ICompCloneable, IStringable, ITuplable {
		ClassT GetClass();
		object ObValue { get; }
		int ID { get; }
	}
}
