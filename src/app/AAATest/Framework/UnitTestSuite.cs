using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace AAATest.Framework
{
	//unit of work for the entire test suite
	public class UnitTestSuite
	{
		//uut = unit under test
		private readonly ReflectionUtil RefUtil;
		private readonly StubCollection Stubs;
		private readonly List<UnitTest> UnitTests;

		public UnitTestSuite(ReflectionUtil util, StubCollection stubs) {
			RefUtil = util;
			Stubs = stubs;
			UnitTests = new List<UnitTest>();
		}

		public void Init(UnitTestSuiteOptions options, params Assembly[] assemblies) {
			var allClasses = new List<Type>();
			foreach (var ass in assemblies)
				allClasses.AddRange(ass.GetTypes());

			FindStubs(allClasses);
			var testClasses = RefUtil.FindClassesByBaseType(allClasses, typeof(Test<>));
			foreach (var test in testClasses) {
				var uutType = RefUtil.GetGenericParameterOfBaseType(test, typeof(Test<>));
				var tests = RefUtil.GetPublicMethods(test);
				foreach (var method in tests) {
					//var testObj = CreateTestObject(test);
					var unitTest = new UnitTest(new DependencyManager(), RefUtil, Stubs) {
						TestClass = test,
						//TestObject = testObj,
						TestMethod = method,
						UnitType = uutType
					};
					UnitTests.Add(unitTest);
				}
			}
		}

		public void Execute()
		{
			foreach (var test in UnitTests)
				test.Execute();
		}

		public void FindStubs(IEnumerable<Type> allTypes)
		{
			var stubMethods = RefUtil.FindGenericImplimentationByName(allTypes, "IStub");
			foreach (var method in stubMethods) {
				Stubs.Add(method.Item1, method.Item2, method.Item3);
			}
		}

		

	}
}
