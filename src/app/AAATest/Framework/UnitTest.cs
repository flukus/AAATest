using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace AAATest.Framework
{
	//unit of work for a single test
	public class UnitTest
	{
		//uut = unit under test
		//dep = dependency

		public Type TestClass { get; set; }
		//public object TestObject { get; set; }
		public Type UnitType { get; set; }
		public ConstructorInfo TestConstructor { get; set; }
		public MethodInfo TestMethod { get; set; }
		public List<Stub> DefaultStubs { get; set; }
		public List<Stub> Stubs { get; set; }
		private readonly DependencyManager DependencyManager;
		private readonly ReflectionUtil RefUtil;
		private readonly StubCollection StubCollection;
		private static readonly ProxyGenerator generator = new ProxyGenerator();

		public UnitTest(DependencyManager depManager, ReflectionUtil refUtil, StubCollection stubCollection) {

			DependencyManager = depManager;
			StubCollection = stubCollection;
			RefUtil = refUtil;
		}

		public void Execute() {
			//create the test class
			//var test = RefUtil.CreateFromEmptyConstructor(TestClass) as ITest;
			

			//create the unit under test
			var uutDepTypes = RefUtil.GetCtorParameters(UnitType);
			var uutDeps = DependencyManager.CreateDependencies(uutDepTypes);
			ApplyStubs(uutDepTypes);
			var uut = RefUtil.CreateTypeWithArguments(UnitType, uutDeps.Select(x=>x.Object).ToArray());

			//execute the test
			var interceptorType = RefUtil.CreateGenericType(typeof(UnitTestExecutionContext<>), UnitType);
			var interceptor = RefUtil.CreateTypeWithArguments(interceptorType, uut, DependencyManager, RefUtil);
			var proxy = generator.CreateClassProxy(TestClass, interceptor as IInterceptor);
			RefUtil.InvokeMethod(proxy, TestMethod);
		}

		public void ApplyStubs(IEnumerable<Type> types) {
			foreach (var type in types)
				ApplyStub(type);
		}

		public void ApplyStub(Type type) {
			var stubsForType = StubCollection.AllStubs.Where(x => x.StubType == type);
			foreach (var stub in stubsForType) {
				var stubber = RefUtil.CreateFromEmptyConstructor(stub.Class);
				var mock = DependencyManager.GetMock(type);
				RefUtil.InvokeMethod(stubber, stub.Method, mock);
			}
		}
		
	}
}
