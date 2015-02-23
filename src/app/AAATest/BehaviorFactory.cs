using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using Moq.Language.Flow;
using System.Linq.Expressions;
using AAATest.Mock;
using System.Reflection;

namespace AAATest {

	public interface IBehaviorFactoryInit {
		void Init(Arranger arranger, Mockery mockery);
	}

	public abstract class BehaviorFactory : IBehaviorFactoryInit, IArrange {

		//private Mockery Dependencies;
		private Arranger Arranger;
		private Mockery Mockery;

		public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) where TMocked : class {
			return Arranger.Arrange<TMocked, TReturn>(expr);
		}

		public IBehavior Arrange<TMocked>(Expression<Action<TMocked>> expr) {
			return Arranger.Arrange<TMocked>(expr);
		}

		public IBehavior<Y> Arrange<Y>(IBehavior<Y> method, Action<Y> action) where Y : class {
			return Arranger.Arrange(method, action);
		}

		public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Y returnValue) {
			return Arranger.Arrange(behavior, returnValue);
		}

		void IBehaviorFactoryInit.Init(Arranger arranger, Mockery mockery) {
			Arranger = arranger;
			Mockery = mockery;
		}

		public T Any<T>() { throw new NotImplementedException(); }
		public T Mock<T>() where T : class {
			return Mockery.GetMock<T>();
		}

		public abstract void Setup();

	}
}
