using System;
using System.Collections.Generic;
namespace MyTypes {
	public class PackageT : List<IItem>, IItem {
		public string Name{ get; private set; }
		public PackageT Package{ get; private set; }

		public PackageT(string name, PackageT package) {
			Name = name;
			Package = package;
		}

		public static PackageT Lang = new PackageT("Lang", null);
	}
}
