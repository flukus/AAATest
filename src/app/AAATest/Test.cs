using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace AAATest {
	public abstract class TestFixture<T>
		where T : class {

		public virtual Mock<Y> Arrange<Y>() where Y : class {
			throw new NotImplementedException();
		}

		public virtual ISetup<Y> Arrange<Y>(Expression<Action<Y>> expr) where Y : class {
			throw new NotImplementedException();
		}

		public virtual ISetup<Y, Z> Arrange<Y, Z>(Expression<Func<Y, Z>> expr) where Y : class {
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

		public virtual Y GetMocked<Y>() where Y : class { throw new NotImplementedException(); }
	}

}
