using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework.Exceptions;
using Castle.DynamicProxy;
using AAATest.Mock;

namespace AAATest.Framework {
	//unit of work for a single test
	public class UnitTest {
		//uut = unit under test
		//dep = dependency

		public Type TestFixtureType { get; set; }
		//public object TestObject { get; set; }
		public Type UnitUnerTestType { get; set; }
		public ConstructorInfo TestFixtureCtor { get; set; }
		public MethodInfo TestMethod { get; set; }
		public PropertyInfo[] BehaviorFactories { get; set; }

		private readonly ReflectionUtil RefUtil;

		public UnitTest(ReflectionUtil refUtil) {
			RefUtil = refUtil;
		}

		public TestCompletedInfo Execute() {

			try {

				//create the unit under test
				var behaviors = new BehaviorCollection();
				var interceptor = new BehaviorInterceptor(behaviors);
				var mockery = new Mockery(interceptor);
				var arranger = new Arranger(behaviors, mockery);
				var uutDepTypes = RefUtil.GetCtorParameters(UnitUnerTestType).ToArray();
				var uutDeps = mockery.GetMocks(uutDepTypes);
				var uut = RefUtil.CreateTypeWithArguments(UnitUnerTestType, uutDeps.ToArray());

				var testFixture = RefUtil.CreateFromEmptyConstructor(TestFixtureType);
				foreach (var bfProperty in BehaviorFactories) {
					var bf = RefUtil.CreateFromEmptyConstructor(bfProperty.PropertyType) as BehaviorFactory;
					var bfinit = bf as IBehaviorFactoryInit;
					bfinit.Init(arranger, mockery);
					bf.Setup();
					bfProperty.SetValue(testFixture, bf);
				}

				//execute the test
				var init = testFixture as ITestFixtureInit;
				init.Init(behaviors, mockery, uut, arranger);
				RefUtil.InvokeMethod(testFixture, TestMethod);

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
				else
					//TODO: should include full trace of all errors
					return new TestCompletedInfo { Result = TestResult.FrameworkError, ErrorName = topMostException.GetType().Name, ErrorMessage = topMostException.Message, StackTrace = topMostException.StackTrace };
			}
		}


	}
}
