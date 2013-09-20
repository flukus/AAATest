using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class TestRunner
	{
		public void Execute(List<UnitTest> tests)
		{
			foreach (var test in tests)
			{
				var activator = new TestActivator();
				var testObj = activator.CreateTest(test);
				test.TestMethod.Invoke(testObj, null);
			}
		}
	}
}
