using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using AAATest.Mock;

namespace AAATest {

    public interface ITestFixtureInit
    {
        void Init(BehaviorCollection behaviors, Mockery depManager, object uut);
    }

	public abstract class TestFixture<T> : ITestFixtureInit, IArrange, IAct<T>
		where T : class {

        private BehaviorCollection Behaviors { get; set; }
        private Mockery Dependencies { get; set; }
        private T UnitUnderTest;
        private object ReturnValue;

        void ITestFixtureInit.Init(BehaviorCollection behaviors, Mockery depManager, object uut)
        {
            Dependencies = depManager;
            Behaviors = behaviors;
            UnitUnderTest = (T)uut;
        }

        public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr)
        {
            var method = (expr as MethodCallExpression).Method;
            var behavior = new Behavior { };
            Behaviors.Add(method, behavior);
            var ret = behavior as IBehavior<TReturn>;
            return ret;
        }

        public IBehavior Arrange<TMocked>(Expression<Action<TMocked>> expr)
        {
            throw new NotImplementedException();
        }

        public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Action<Y> action) where Y : class
        {
            var realBahvior = behavior as Behavior;
            action((Y)realBahvior.ReturnValue);
            return behavior;
        }

        public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Y returnValue)
        {
            var realBahvior = behavior as Behavior;
            realBahvior.ReturnValue = returnValue;
            return behavior;
        }

		public virtual void Act(Action<T> action) {
            action(UnitUnderTest);
		}

		public virtual void Act<A>(Func<T, A> action) {
            ReturnValue = action(UnitUnderTest);
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
			throw new NotImplementedException();
		}

		public virtual void AssertException<TException>() where TException : Exception {
			throw new NotImplementedException();
		}

		public virtual void AssertException<TException>(string message) where TException : Exception {
			throw new NotImplementedException();
		}

        public void Assert(IBehavior method){}
        public void Assert(IBehavior method, int count){}

		public virtual Y GetMocked<Y>() where Y : class { throw new NotImplementedException(); }

        public static T Any<T>() 
        {
            return default(T);
        }

        public static T Where<T>(Func<T, bool> func)
        {
            return default(T);
        }

        public static Expression<Func<T, TReturn>> AnyExpr<T, TReturn>() { return null; }
	}

}
