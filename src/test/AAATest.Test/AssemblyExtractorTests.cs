using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.Framework;

namespace AAATest.ExampleTests
{
	public class AssemblyExtractorTests : Test<ReflectionUtil>
	{

		public void TestTestsForwardedToClassExtractor()
		{
			//Arrange<ClassExtractor>(x=>x.ExtractTest()).Verifiable();
			//Act(x => x.ExtractTests(new Assembly[] { this.GetType().Assembly }));
			//Assert();
		}
	}
}
