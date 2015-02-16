using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using AAATest.Mock;

namespace AAATest {

	public abstract class TestFixture<T> : IArrange
		where T : class {

        public IReturns<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr)
        {
            throw new NotImplementedException();
        }

        public IMethodStub Arrange<TMocked>(Expression<Action<TMocked>> expr)
        {
            throw new NotImplementedException();
        }

        public IReturns<Y> Arrange<Y>(IReturns<Y> method, Action<Y> action) where Y : class
        {
            throw new NotImplementedException();
        }

		public virtual void Act(Action<T> action) {
			throw new NotImplementedException();
		}

		public virtual void Act<A>(Func<T, A> action) {
			throw new NotImplementedException();
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

        public void Assert(IMethodStub method){}
        public void Assert(IMethodStub method, int count){}

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
