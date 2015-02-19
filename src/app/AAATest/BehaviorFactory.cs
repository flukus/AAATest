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
		void Init(Arranger arranger);
	}

	public abstract class BehaviorFactory : IBehaviorFactoryInit, IArrange {

		//private Mockery Dependencies;
		private Arranger Arranger;

		public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) {
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

		void IBehaviorFactoryInit.Init(Arranger arranger) {
			Arranger = arranger;
		}

		public T Any<T>() { throw new NotImplementedException(); }

		public abstract void Setup();

	}
}
