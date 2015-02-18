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
        private Dictionary<MethodInfo, List<IBehavior>> Behaviors;

        public BehaviorCollection()
        {
            Behaviors = new Dictionary<MethodInfo, List<IBehavior>>();
        }

        public void Add(MethodInfo method, Behavior behavior)
        {
            List<IBehavior> behaviors = null;
            if (!Behaviors.ContainsKey(method))
            {
                behaviors = new List<IBehavior>();
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
    }
}
