using System;
using System.Collections.Generic;

namespace MyTypes {
	class ObjectContainer : List<IVariable> {
		private ObjectContainer() {
		}

		private static ObjectContainer instance;

		public static ObjectContainer Instance { 
			get {
				return instance==null ? instance=new ObjectContainer() : instance;
			}
		 	private set {} 
		}

		public new int Add(IVariable iv) {
			base.Add(iv);
			return Count-1;
		}
	}
}
