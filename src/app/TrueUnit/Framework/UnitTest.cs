using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class UnitTest
	{
		public Type TestClass { get; set; }
		public Type UnitType { get; set; }
		public ConstructorInfo TestConstructor { get; set; }
		public MethodInfo TestMethod { get; set; }
		public List<Stub> DefaultStubs { get; set; }
		public List<Stub> Stubs { get; set; }
	}
}
