using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class TestActivator
	{
		public ITest CreateTest(UnitTest test)
		{
			if (test.TestConstructor == null)
				throw new Exception("Cannot instantiate test because it does not have an empty or default constructor");
			var testObj = test.TestConstructor.Invoke(null) as ITest;
			var helper = new TestHelper(test, new DependencyManager(test));
			testObj.Initialize(helper);
			return testObj;
		}
	}
}
