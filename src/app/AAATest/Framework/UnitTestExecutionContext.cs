using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Moq;
using Moq.Language.Flow;

namespace AAATest.Framework {
	class UnitTestExecutionContext<T> : Test<T>, IInterceptor where T : class {

		private readonly DependencyManager DependencyManager;

		/// <summary>
		/// The unit under test
		/// </summary>
		public object Unit { get; set; }
		private TestState CurrentStage;
		private bool ExpectVoidResult;
		private object Result { get; set; }

		public UnitTestExecutionContext(object unit, DependencyManager depManager, ReflectionUtil refHelp) {

			Unit = unit;
			DependencyManager = depManager;
			CurrentStage = TestState.None;
		}

		public void Intercept(IInvocation invocation) {
			//only intercept methods declared on Test<>
			var type = invocation.MethodInvocationTarget.DeclaringType;
			var method = invocation.MethodInvocationTarget;
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Test<>)) {
				//this inherits from Test<T> but is not a proxy itself, so we can redirect these method invocations to this
				invocation.ReturnValue = method.Invoke(this, invocation.Arguments);
			} else
				invocation.Proceed();
		}

		public override Mock<Y> Arrange<Y>() {
			return DependencyManager.GetMock<Y>(); 
		}

		public override ISetup<Y> Arrange<Y>(System.Linq.Expressions.Expression<Action<Y>> expr) {
			var mock = DependencyManager.GetMock<Y>();
			return mock.Setup(expr);
		}

		public override ISetup<Y, Z> Arrange<Y, Z>(System.Linq.Expressions.Expression<Func<Y, Z>> expr) {
			var mock = DependencyManager.GetMock<Y>();
			var setup = mock.Setup(expr);
			return setup;
		}

		public override void Act(Action<T> action) {
			action((T)Unit);
			ExpectVoidResult = true;
		}

		public override void Act<A>(Func<T, A> action) {
			Result = action((T)Unit);
		}

		public override void Assert() {
			foreach (var mock in DependencyManager.AllMocks())
				mock.Verify();
		}

		public override Assert<Z> Assert<Y, Z>(Func<Y, object> func) {
			if (Result == null)
				throw new NullReferenceException("Execution Result was null");
			var typedResult = Result as Y;
			if (typedResult == null)
				throw new InvalidCastException(string.Format("Execution Result was not of type {0}", typeof(Y)));
			var funcResult = func(typedResult);
			if (funcResult == null)
				throw new NullReferenceException("Nested execution Result was null");
			var typedFuncResult = funcResult as Z;
			if (typedFuncResult == null)
				throw new InvalidCastException(string.Format("Nested execution Result was not of type {0}", typeof(Y)));
			return new Assert<Z>(typedFuncResult);
			
		}

		public override Assert<Y> Assert<Y>() {
			Y result = (Y)Result;
			return new Assert<Y>(result);
		}

		public override void AssertException(string message) {
			throw new NotImplementedException();
		}

		public override void AssertException<TException>() {
			throw new NotImplementedException();
		}

		public override void AssertException<TException>(string message) {
			throw new NotImplementedException();
		}
	}
}
