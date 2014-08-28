using System;
using Moq;
using Moq.Language.Flow;

namespace AAATest.Framework {

	public interface IAAATest<T> {
		Mock<Y> Arrange<Y>() where Y : class;
		ISetup<Y> Arrange<Y>(System.Linq.Expressions.Expression<Action<Y>> expr) where Y : class;
		ISetup<Y, Z> Arrange<Y, Z>(System.Linq.Expressions.Expression<Func<Y, Z>> expr) where Y : class;
		void Act(Action<T> action);
		void Act<A>(Func<T, A> action);
		void Assert();
		Assert<Z> Assert<Y, Z>(Func<Y, object> func) where Y : class where Z : class;
		Assert<Y> Assert<Y>();
		void AssertException(string message);
		void AssertException<TException>();
		void AssertException<TException>(string message);
	}
}
