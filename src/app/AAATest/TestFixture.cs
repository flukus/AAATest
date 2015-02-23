using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using AAATest.Framework.Exceptions;
using AAATest.Mock;

namespace AAATest {

	public interface ITestFixtureInit {
		void Init(BehaviorCollection behaviors, Mockery depManager, object uut, Arranger arranger);
	}

	public abstract class TestFixture<T> : ITestFixtureInit, IArrange, IAct<T>
		where T : class {

		private BehaviorCollection Behaviors { get; set; }
		private Mockery Dependencies { get; set; }
		private Arranger Arranger;
		private T UnitUnderTest;
		private object ReturnValue;
		private Exception ActException;

		void ITestFixtureInit.Init(BehaviorCollection behaviors, Mockery depManager, object uut, Arranger arranger) {
			Dependencies = depManager;
			Behaviors = behaviors;
			UnitUnderTest = (T)uut;
			Arranger = arranger;
		}

		public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) where TMocked : class {
			return Arranger.Arrange<TMocked, TReturn>(expr);
		}

		public IBehavior Arrange<TMocked>(Expression<Action<TMocked>> expr) {
			return Arranger.Arrange<TMocked>(expr);
		}

		public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Action<Y> action) where Y : class {
			return Arranger.Arrange(behavior, action);
		}

		public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Y returnValue) {
			return Arranger.Arrange(behavior, returnValue);
		}

		public virtual void Act(Action<T> action) {
			try {
				action(UnitUnderTest);
			} catch (Exception e) {
				ActException = e;
			}
		}

		public virtual void Act<A>(Func<T, A> action) {
			try {
				ReturnValue = action(UnitUnderTest);
			} catch (Exception e) {
				ActException = e;
			}
		}

		public virtual void Assert() {
			throw new NotImplementedException();
		}

		public virtual Assert<Z> Assert<Y, Z>(Func<Y, object> func)
			where Y : class
			where Z : class {
			if (ReturnValue == null)
				throw new AssertException("Returned Value was null");
			var value = ReturnValue as Y;
			if (value == null)
				throw new AssertException(string.Format("Returned value expected to be type '{0}' but was type '{1}'", typeof(Y), ReturnValue.GetType()));
			var funcResult = func(value);
			if (funcResult == null)
				throw new AssertException("Expecting a value but result was null");
			var typedFuncResult = funcResult as Z;
			if (value == null)
				throw new AssertException(string.Format("Returned value expected to be type '{0}' but was type '{1}'", typeof(Z), funcResult.GetType()));
			return new Assert<Z>(typedFuncResult);
		}

		public virtual Assert<Y> Assert<Y>() {
			throw new NotImplementedException();
		}

		public virtual void AssertException(string message) {
			if (ActException == null)
				throw new AssertException("Expected exception but none was thrown");
			if (ActException.Message != message)
				throw new AssertException(string.Format("Expected exception message of '{0}' but result was '{1}'", message, ActException.Message));
		}

		public virtual void AssertException<TException>() where TException : Exception {
			if (ActException == null)
				throw new AssertException("Expected exception but none was thrown");
		}

		public virtual void AssertException<TException>(string message) where TException : Exception {
			if (ActException == null)
				throw new AssertException("Expected exception but none was thrown");
			if (ActException.GetType() != typeof(TException))
				throw new AssertException(string.Format("Expected exception of type of '{0}' but result was '{1'}", typeof(TException).Name, ActException.GetType().Name));
			if (ActException.Message != message)
				throw new AssertException(string.Format("Expected exception message of '{0}' but result was '{1'}", message, ActException.Message));
		}

		public void Assert(IBehavior method) {
			var behavior = method as Behavior;
			if (behavior.CallCount == 0)
				throw new AssertException("Expected behavior to be called but it wasn't");
		}
		public void Assert(IBehavior method, int count) {
			var behavior = method as Behavior;
			if (behavior.CallCount == count)
				throw new AssertException(string.Format("Expected behavior to be called {0} times but it was called {1} times", count, behavior.CallCount));
		}

		public virtual Y GetMocked<Y>() where Y : class { throw new NotImplementedException(); }

		public static TMock Any<TMock>() {
			return default(TMock);
		}

		public static TMock Where<TMock>(Func<T, bool> func) {
			return default(TMock);
		}

		public static Expression<Func<T, TReturn>> AnyExpr<T, TReturn>() { return null; }
	}

}
