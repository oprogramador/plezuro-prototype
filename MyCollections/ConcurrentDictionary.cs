using System;
using System.Collections.Generic;
namespace MyCollections {
	public class ConcurrentDictionary<TKey, TValue> : Dictionary<TKey, TValue> {
		public bool TryAdd(TKey key, TValue val) {
			if(ContainsKey(key)) return false;
			this[key] = val;
			return true;
		}
	}
}
