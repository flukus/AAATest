using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using AAATest.Framework;

namespace AAATest.Framework
{
	//unit of work for the entire test suite
	public class UnitTestSuite
	{
		//uut = unit under test
		private readonly ReflectionUtil RefUtil;
		private readonly StubCollection Stubs;
		private readonly ITestResultListener Listener;
		private readonly List<UnitTest> UnitTests;

		public UnitTestSuite(ReflectionUtil util, StubCollection stubs, ITestResultListener listener) {
			RefUtil = util;
			Stubs = stubs;
			Listener = listener;
			UnitTests = new List<UnitTest>();
		}

		public void Init(UnitTestSuiteOptions options, params Assembly[] assemblies) {
			var allClasses = new List<Type>();
			foreach (var ass in assemblies) {
				System.Console.WriteLine("ScanningAssembly " + ass.FullName);
				Type[] types = null;
				try {
					types = ass.GetTypes();
				} catch (ReflectionTypeLoadException e) {
					types = e.Types.Where(x => x != null).ToArray();
				}
				allClasses.AddRange(types);
			}
			System.Console.WriteLine(allClasses.Count);

			FindStubs(allClasses);
			var testClasses = RefUtil.FindClassesByBaseType(allClasses, typeof(TestFixture<>));
			foreach (var test in testClasses) {
				var uutType = RefUtil.GetGenericParameterOfBaseType(test, typeof(TestFixture<>));
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

		public bool Execute()
		{
			bool suiteResult = true; //passed
			foreach (var test in UnitTests) {
				Listener.TestStarted(string.Format("{0}.{1}", test.TestClass.Name, test.TestMethod.Name));
				var result = test.Execute();
				Listener.TestComplete(result);
				suiteResult &= result.Result == TestResult.Passed;
			}

			Listener.AllTestsComplete();
			return suiteResult;
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
