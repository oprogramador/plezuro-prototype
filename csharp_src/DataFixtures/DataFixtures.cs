/*
 * DataFixtures.cs
 * Copyright 2014 pierre (Piotr Sroczkowski) <pierre.github@gmail.com>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301, USA.
 * 
 * 
 */
 

using System;
using System.Collections.Generic;
using System.IO;
using MyTypes.MyClasses;

namespace DataFixtures {
	class DataFixtures : SetT {
		private static DataFixtures instance;

		public static DataFixtures GetInstance() { 
			if(instance==null) instance = new DataFixtures();
			return instance;
		}

		private void load() {
			var sr = new StreamReader("res/world.db");
			string line;
			while((line=sr.ReadLine()) != null) {
				var ar = line.Split('\t');
				var record = new Dictionary<object,object>();
				for(int i=0; i+1<ar.Length; i+=2) record[ar[i]] = ar[i+1];
				Console.WriteLine("record="+record);
				Console.WriteLine("record.count="+record.Count);
				Console.WriteLine("ar.length="+ar.Length);
				foreach(var i in record) Console.WriteLine("key: "+i.Key+" value: "+i.Value);
				Add(new DictionaryT(record));
			}
		}

		private DataFixtures() {
			try {
				load();
			} catch(Exception e){
				Console.WriteLine("error with reading database file e: "+e);
			}
		}
	}
}
