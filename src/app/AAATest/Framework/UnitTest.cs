using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework.Exceptions;
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

		public TestCompletedInfo Execute() {

			try {

				//create the unit under test
				var uutDepTypes = RefUtil.GetCtorParameters(UnitType);
				var uutDeps = DependencyManager.CreateDependencies(uutDepTypes);
				ApplyStubs(uutDepTypes);
				//var uut = RefUtil.CreateTypeWithArguments(UnitType, uutDeps.Select(x => x.Object).ToArray());

				//execute the test
				var interceptorType = RefUtil.CreateGenericType(typeof(UnitTestExecutionContext<>), UnitType);
				//var interceptor = RefUtil.CreateTypeWithArguments(interceptorType, uut, DependencyManager);
				//var proxy = generator.CreateClassProxy(TestClass, interceptor as IInterceptor);
				//RefUtil.InvokeMethod(proxy, TestMethod);

				return new TestCompletedInfo { Result = TestResult.Passed };
			} catch (Exception e) { //TODO: catch more exception types here
				//not interested in targetInvocationExceptions, it's just a wrapper around the real one, only use that if it's the inner most exception
				//need the second top most stacktrace, which contains the user relevant exception
				//Method.invoke doesn't throw the original section and makes life a pain in the ass, would rather have this logic in listener
				Exception topMostException = e;
				Exception outerException = e;
				while (e.GetType() == typeof(TargetInvocationException) && e.InnerException != null)
					e = e.InnerException;

				if (e.GetType() == typeof(AssertException))
					return new TestCompletedInfo { Result = TestResult.Failed, ErrorName = "Assert Failure", ErrorMessage = e.Message, StackTrace = outerException.StackTrace };
				else if (e.GetType().Name == "MockVerificationException")
					return new TestCompletedInfo { Result = TestResult.Failed, ErrorName = "Assert Failure", ErrorMessage = e.Message, StackTrace = topMostException.InnerException.StackTrace };
				else
					//TODO: should include full trace of all errors
					return new TestCompletedInfo { Result = TestResult.FrameworkError, ErrorName = topMostException.GetType().Name, ErrorMessage = topMostException.Message, StackTrace = topMostException.StackTrace };
			}
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
