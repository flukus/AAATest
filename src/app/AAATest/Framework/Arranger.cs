using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AAATest.Mock;

namespace AAATest.Framework {
	public class Arranger : IArrange {

		private readonly BehaviorCollection Behaviors;

		public Arranger(BehaviorCollection behaviors) {
			Behaviors = behaviors;
		}

		public Mock.IBehavior<TReturn> Arrange<TMocked, TReturn>(System.Linq.Expressions.Expression<Func<TMocked, TReturn>> expr) {
			var method = (expr as MethodCallExpression).Method;
			var behavior = new Behavior { };
			Behaviors.Add(method, behavior);
			var ret = behavior as IBehavior<TReturn>;
			return ret;
		}

		public Mock.IBehavior Arrange<TMocked>(System.Linq.Expressions.Expression<Action<TMocked>> expr) {
			throw new NotImplementedException();
		}

		public Mock.IBehavior<Y> Arrange<Y>(Mock.IBehavior<Y> behavior, Action<Y> action) where Y : class {
			var realBahvior = behavior as Behavior;
			action((Y)realBahvior.ReturnValue);
			return behavior;
		}

		public Mock.IBehavior<Y> Arrange<Y>(Mock.IBehavior<Y> behavior, Y returnValue) {
			var realBahvior = behavior as Behavior;
			realBahvior.ReturnValue = returnValue;
			return behavior;
		}
	}
}
