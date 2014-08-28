using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework
{
	public class TestHelper //: IAAATest<Task>
	{
		private object Result { get; set; }
		private bool ExpectVoidResult { get; set; }
		private UnitTest UnitTest { get; set; }
		private DependencyManager DependencyManager { get; set; }
		private TestState CurrentState;

		public TestHelper(UnitTest unitTest, DependencyManager depManager)
		{
			UnitTest = unitTest;
			DependencyManager = depManager;
			CurrentState = TestState.None;
		}

		public void Init() {

		}

		public Mock<Y> Arrange<Y>() where Y : class { return DependencyManager.GetMock<Y>(); }

		public ISetup<Y> Arrange<Y>(Expression<Action<Y>> expr) where Y : class
		{
			var mock = DependencyManager.GetMock<Y>();
			return mock.Setup(expr);
		}

		public ISetup<Y, Z> Arrange<Y, Z>(Expression<Func<Y, Z>> expr) where Y : class
		{
			var mock = DependencyManager.GetMock<Y>();
			return mock.Setup(expr);
		}

		public void Act<T>(Action<T> action) {
			//action((T)"");
			ExpectVoidResult = true;
		}

		public void Act<T, A>(Func<T, A> action)
		{
			//Result = action((T)unit);
		}

		public void Assert()
		{
			foreach (var mock in DependencyManager.AllMocks())
				mock.Verify();
		}

		public Assert<Y> Assert<Y>()
		{
			Y result = (Y)Result;
			return new Assert<Y>(result);
		}

		public Assert<Z> Assert<Y, Z>(Func<Y, object> func)
			where Y : class
			where Z : class
		{
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

		public void AssertException(string message)
		{
		}

		public void AssertException<TException>()
		{
		}

		public void AssertException<TException>(string message)
		{
		}

	}
}
