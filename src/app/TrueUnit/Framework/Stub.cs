using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class Stub
	{
		public Type Class { get; set; }
		public Type StubType { get; set; }
		public ConstructorInfo Constructor { get; set; }
		public MethodInfo Method { get; set; }
	}
}
