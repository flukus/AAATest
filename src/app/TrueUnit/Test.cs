using Moq;
using Moq.Language.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrueUnit.Framework;

namespace TrueUnit
{
	public abstract class Test<T> : ITest
		where T : class
	{
		private TestHelper Helper;

		void ITest.Initialize(TestHelper helper)
		{
			Helper = helper;
		}

		protected Mock<Y> Arrange<Y>() where Y : class
		{
			return Helper.Arrange<Y>();
		}

		protected ISetup<Y> Arrange<Y>(Expression<Action<Y>> expr) where Y : class
		{
			return Helper.Arrange<Y>(expr);
		}

		protected ISetup<Y, Z> Arrange<Y, Z>(Expression<Func<Y, Z>> expr) where Y : class
		{
			return Helper.Arrange<Y, Z>(expr);
		}

		protected void Act(Action<T> action)
		{
			Helper.Act<T>(action);
		}

		protected void Act<A>(Func<T, A> action)
		{
			Helper.Act<T, A>(action);
		}

		protected Assert<Y> Assert<Y>()
		{
			return Helper.Assert<Y>();
		}

		protected Assert<Z> Assert<Y, Z>(Func<Y, object> func)
			where Y : class
			where Z : class
		{
			return Helper.Assert<Y, Z>(func);
		}

		protected void AssertException(string message)
		{
			Helper.AssertException(message);
		}

		protected void AssertException<TException>()
		{
			Helper.AssertException<TException>();
		}

		protected void AssertException<TException>(string message)
		{
			Helper.AssertException<TException>(message);
		}

	}

}
