using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework {
	public class StubCollection {

		public List<Stub> AllStubs { get; set; }

		public StubCollection() {
			AllStubs = new List<Stub>();
		}

		public void Add(Type actualType, Type stubType, MethodInfo method) {
			AllStubs.Add(new Stub { Class = actualType, StubType = stubType, Method = method });
		}

		

	}
}
