using AAATest.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class BehaviorCollection
    {
        private Dictionary<MethodInfo, List<Behavior>> Behaviors;

        public BehaviorCollection()
        {
            Behaviors = new Dictionary<MethodInfo, List<Behavior>>();
        }

        public void Add(MethodInfo method, Behavior behavior)
        {
            List<Behavior> behaviors = null;
            if (!Behaviors.ContainsKey(method))
            {
                behaviors = new List<Behavior>();
                Behaviors[method] = behaviors;
            }
            else 
                behaviors = Behaviors[method];
            behaviors.Add(behavior);

        }

        public Mock.IBehavior Arrange<TMocked>(System.Linq.Expressions.Expression<Action<TMocked>> expr)
        {
            throw new NotImplementedException();
        }

        public Mock.IBehavior<Y> Arrange<Y>(Mock.IBehavior<Y> method, Action<Y> action) where Y : class
        {
            throw new NotImplementedException();
        }

        public Mock.IBehavior<Y> Arrange<Y>(Mock.IBehavior<Y> behavior, Y returnValue)
        {
            throw new NotImplementedException();
        }

				internal List<Behavior> GetBehaviorsForMethod(MethodInfo methodInfo) {
					if (Behaviors.ContainsKey(methodInfo))
						return Behaviors[methodInfo];
					else return new List<Behavior>();
				}
		}
}
