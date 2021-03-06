﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using AAATest.Framework;

namespace AAATest.Framework {
	//unit of work for the entire test suite
	public class UnitTestSuite {
		//uut = unit under test
		private readonly ReflectionUtil RefUtil;
		private readonly List<Type> BehaviorFactories;
		private readonly ITestResultListener Listener;
		private readonly List<UnitTest> UnitTests;

		public UnitTestSuite(ReflectionUtil util, ITestResultListener listener) {
			RefUtil = util;
			BehaviorFactories = new List<Type>();
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

				//get the behavior factories needed for each test fixture
				var bf = test.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
					.Where(x => (x.CanWrite && typeof(BehaviorFactory).IsAssignableFrom(x.PropertyType))).ToArray();

				//find the actual tests
				var tests = RefUtil.GetPublicMethods(test).Where(x => !x.IsSpecialName);
				foreach (var method in tests) {
					//var testObj = CreateTestObject(test);
					var unitTest = new UnitTest(RefUtil) {
						TestFixtureType = test,
						//TestObject = testObj,
						TestMethod = method,
						UnitUnerTestType = uutType,
						BehaviorFactories = bf
					};
					UnitTests.Add(unitTest);
				}

			}
		}

		public bool Execute() {
			bool suiteResult = true; //passed
			foreach (var test in UnitTests) {
				Listener.TestStarted(string.Format("{0}.{1}", test.TestFixtureType.Name, test.TestMethod.Name));
				var result = test.Execute();
				Listener.TestComplete(result);
				suiteResult &= result.Result == TestResult.Passed;
			}

			Listener.AllTestsComplete();
			return suiteResult;
		}

		public void FindStubs(IEnumerable<Type> allTypes) {
			var behaviorFactories = allTypes.Where(x => typeof(BehaviorFactory).IsAssignableFrom(x));
			BehaviorFactories.AddRange(behaviorFactories);
			//var stubMethods = RefUtil.FindGenericImplimentationByName(allTypes, "IStub");
			//foreach (var method in stubMethods) {
			//Stubs.Add(method.Item1, method.Item2, method.Item3);
			//}
		}



	}
}
