using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAAUnit.Framework.Exceptions;

namespace AAATest.Mock {
	public class BehaviorInterceptor : IInterceptor {

		private readonly BehaviorCollection Behaviors;

		public BehaviorInterceptor(BehaviorCollection behaviors) {
			Behaviors = behaviors;
		}

		public void Intercept(IInvocation invocation) {
			try {
				Replay(invocation);
				//invocation.Proceed();
			} catch (Exception) {
				throw;
			} finally {
			}
		}

		private void Replay(IInvocation invocation) {
			var behaviors = Behaviors.GetBehaviorsForMethod(invocation.Method);
			if (behaviors.Count() == 0)
				throw new Exception(string.Format("No behaviors found for method {0}", invocation.Method.Name));
			foreach (var behavior in behaviors) {
				if (behavior.IsMatch(invocation.Arguments)) {
					behavior.CallCount++;
					invocation.ReturnValue = behavior.ReturnValue;
					return;
				}

			}
		}

	}
}
