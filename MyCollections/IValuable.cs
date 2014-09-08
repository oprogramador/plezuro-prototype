using System;
using System.Collections.Generic;
namespace MyCollections {
	interface IValuable {
		object Value {
			get;
			set;
		}

		object Construct(object ob);
	}
}
