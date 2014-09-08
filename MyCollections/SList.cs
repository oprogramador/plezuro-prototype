using System.Collections;
using System;
using System.Collections.Generic;using MyTypes;
using MyTypes.MyClasses;

namespace MyCollections {
	public class SList<T> : CList<T>, ICompCloneable where T : ICompCloneable {
		
		public SList() : base() {

		}

		public SList(IEnumerable ie) {
			foreach(var item in ie)  {
				Add((T)(item is ICompCloneable ? ((ICompCloneable)item).Clone() : Variable.Convert(item)));
			}
		}
		
		public virtual object Clone() {
			return new SList<T>(this);
		}

		public static implicit operator SList<T>(List<object> l) {
			return new SList<T>(l);
		}

		public SList<T> Concat(SList<T> st) {
			var ret = new SList<T>(this);
			ret.AddRange(st);
			return ret;
		}

	}
}
