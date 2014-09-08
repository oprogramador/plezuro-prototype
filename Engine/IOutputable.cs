using System;
using System.Collections.Generic;using MyCollections;

namespace Engine {
	interface IOutputable : ITextable {
		void ShowError(bool b);
	}
}
