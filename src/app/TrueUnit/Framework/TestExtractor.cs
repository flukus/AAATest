using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class TestExtractor
	{
		public List<UnitTest> ExtractTests(IEnumerable<Assembly> assemblies)
		{
			var tests = new List<UnitTest>();
			foreach (var ass in assemblies)
			{
				var defaultStubs = ExtractDefaultStubs(ass);
				tests.AddRange(ExtractTests(ass, defaultStubs));
			}

			return tests;
		}

		public List<UnitTest> ExtractTests(Assembly assembly, List<Stub> defaultStubs)
		{
			var tests = new List<UnitTest>();

			//get all the test methods
			var testMethods = new List<MethodInfo>();
			var testClasses = assembly.GetTypes()
				.Where(x => x.IsTest());
			foreach (var klass in testClasses)
				testMethods.AddRange(klass.GetMethods().Where(y => y.IsTestMethod()));

			foreach (var testMethod in testMethods)
			{
				var test = new UnitTest();
				test.TestMethod = testMethod;
				test.TestClass = testMethod.DeclaringType;
				test.TestConstructor = testMethod.DeclaringType.GetConstructor(new Type[0]);;
				test.UnitType = testMethod.DeclaringType.BaseType.GetGenericArguments().First();
				test.DefaultStubs = defaultStubs;
				test.Stubs = ExtractStubs(testMethod.DeclaringType);
				tests.Add(test);
			}

			return tests;
		}

		public List<Stub> ExtractDefaultStubs(Assembly assembly)
		{
			var stubs = new List<Stub>();
			foreach (var type in assembly.GetTypes())
				if (!type.IsTest())
					stubs.AddRange(ExtractStubs(type));
			return stubs;
		}

		public List<Stub> ExtractStubs(Type type)
		{
			var stubs = new List<Stub>();
			var stubInterfaces = type.GetInterfaces()
				.Where(x=> x.Name.StartsWith("IStub"));
			foreach (var stubInterface in stubInterfaces)
			{
				var stubType = stubInterface.GetGenericArguments().First();
				var map = type.GetInterfaceMap(stubInterface);
				var method = map.InterfaceMethods.FirstOrDefault();
				var ctor = type.GetConstructor(new Type[0]);
				var stub = new Stub
				{
					Class = type,
					Method = method,
					Constructor = ctor,
					StubType = stubType
				};
				stubs.Add(stub);
			}
			return stubs;
		}

	}
}
