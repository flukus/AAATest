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
		void Init(BehaviorCollection behaviors, Mockery depManager, object uut);
	}

	public abstract class TestFixture<T> : ITestFixtureInit, IArrange, IAct<T>
		where T : class {

		private BehaviorCollection Behaviors { get; set; }
		private Mockery Dependencies { get; set; }
		private Arranger Arranger;
		private T UnitUnderTest;
		private object ReturnValue;
		private Exception ActException;

		void ITestFixtureInit.Init(BehaviorCollection behaviors, Mockery depManager, object uut) {
			Dependencies = depManager;
			Behaviors = behaviors;
			UnitUnderTest = (T)uut;
		}

		public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) {
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
			throw new NotImplementedException();
		}

		public virtual Assert<Y> Assert<Y>() {
			throw new NotImplementedException();
		}

		public virtual void AssertException(string message) {
			if (ActException == null)
				throw new AssertException("Expected exception but none was thrown");
			if (ActException.Message != message)
				throw new AssertException(string.Format("Expected exception message of '{0}' but result was '{1'}", message, ActException.Message));
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

		public void Assert(IBehavior method) { }
		public void Assert(IBehavior method, int count) { }

		public virtual Y GetMocked<Y>() where Y : class { throw new NotImplementedException(); }

		public static T Any<T>() {
			return default(T);
		}

		public static T Where<T>(Func<T, bool> func) {
			return default(T);
		}

		public static Expression<Func<T, TReturn>> AnyExpr<T, TReturn>() { return null; }
	}

}
