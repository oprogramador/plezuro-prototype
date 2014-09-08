using System;
using System.Collections.Generic;

namespace MyTypes {
	public interface IItem {
		string Name{ get; }
		PackageT Package{ get; }
	}
}
