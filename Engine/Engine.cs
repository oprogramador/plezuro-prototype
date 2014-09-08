using System;
using System.Collections.Generic;
namespace Engine {
	class Engine {
		private static Engine instance;
		public SymbolMap SymbolMap { get; private set; }
		public IOMap IOMap { get; private set; }

		private Engine() {
			SymbolMap = new SymbolMap();
			IOMap = new IOMap();
		}

		public static Engine GetInstance() {
			if(instance==null) instance = new Engine();
			return instance;
		}
	}
}
