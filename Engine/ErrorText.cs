using System;
using System.Collections.Generic;
using MyCollections;
using MyTypes;
using MyTypes.MyClasses;

namespace Engine {
	class ErrorText {
		private static readonly Dictionary<Type, string> dic = new Dictionary<Type, string> { {typeof(InvalidOperationException), "Error, invalid syntax."}, {typeof(EmptyArgException), "Error, empty set."},
		};

		public Exception Exception { get; private set; }

		public ErrorText(Exception e) {
			Exception = e;
		}

		public override string ToString() {
			try {
				return dic[Exception.GetType()];
			} catch {
				return ""+Exception;
			}
		}
	}
}
