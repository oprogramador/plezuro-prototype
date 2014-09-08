using System;
using System.Collections.Generic;
namespace MyCollections {
	public class CList<T> : WList<T> where T: IComparable {
		public int CompareTo(object ob) {
			var sl = ob as SList<ICompCloneable>;
			if(sl==null) return 1;
			for(int i=0;; i++) {
				if(i>=Count && i<sl.Count) return -1;
				if(i<Count && i>=sl.Count) return 1;
				if(i>=Count && i>=sl.Count) break;
				int cmp = this[i].CompareTo(sl[i]);
				if(cmp!=0) return cmp;
			}
			return 0;
		}

		public T MaxMin(bool ismax) {
			if(Count==0) throw new EmptyArgException();
			var max = this[0];
			for(int i=1; i<Count; i++) if(this[i].CompareTo(max)>0 == ismax) max=this[i];
			return max;
		}

		public T Max() {
			return MaxMin(true);
		}
		
		public T Min() {
			return MaxMin(false);
		}

		public T Median() {
			T[] array = ToArray();
			int pos = median(array, 0, Count-1, Count/2);
			if(Count%2==1) return array[pos];
			//if(array[pos] is double && array[pos-1] is double) return 0.5*(((double)array[pos])+((double)array[pos-1]));
			return array[pos];
		}

		int partition(T[] a, int first, int last) {
			int p=last, index=first;
		    	for(int i=first; i<last; i++) if (a[i].CompareTo(a[p])<0) {
			    Swap(ref a[i], ref a[index]);
			    index++;
			}
		    	Swap(ref a[p], ref a[index]);
		    	return index;
		}

		int median(T[] a, int first, int last, int mid) {
			if(first==last) return first;
			int p = partition(a, first, last);
			if(p == mid) return p;
			if(p > mid) return median(a, first, p - 1, mid);
			else return median(a, p + 1, last, mid);
		}

		public static void Swap<TT>(ref TT a, ref TT b) {
			TT c = a;
			a = b;
			b = c;
		}

	}
}
