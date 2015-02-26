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
		private readonly Mockery Mockery;

		public Arranger(BehaviorCollection behaviors, Mockery mockery) {
			Behaviors = behaviors;
			Mockery = mockery;
		}

		public Mock.IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) where TMocked : class {
			var mock = Mockery.GetMock<TMocked>();
			var method = (expr.Body as MethodCallExpression).Method;
			var behavior = new Behavior<TReturn>(mock, Mockery);
			//foreach (var param in method.GetParameters()) {
				//behavior.Matchers.Add(new Matcher());
			//}
			//Behaviors.Add(method, behavior);
			//return behavior;

			var body = expr.Body;
			if (body.NodeType == ExpressionType.Call) {
				var callExpr = body as MethodCallExpression;
				var matchers = new List<Matcher>();
				foreach (var arg in callExpr.Arguments) {
					if (arg.NodeType == ExpressionType.Constant) {
						var c = arg as ConstantExpression;
						matchers.Add(new ValueMatcher(c.Value));
					} else if (arg.NodeType == ExpressionType.Call) {
						var c = arg as MethodCallExpression;
						//assume only matcher is any
						if (c.Arguments.Count() == 0)
							matchers.Add(new Matcher());
						else if (c.Arguments[0].NodeType == ExpressionType.Lambda) {
							var lamda = c.Arguments[0] as LambdaExpression;
							var d = lamda.Compile();
							matchers.Add(new DelegateMatcher(d));
						}
					}
					behavior.Matchers.AddRange(matchers);
				}
				Behaviors.Add(callExpr.Method, behavior);
			}
			return behavior;
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
