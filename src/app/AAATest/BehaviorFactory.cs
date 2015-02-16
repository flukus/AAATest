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

namespace AAATest
{

    public interface IBehaviorFactoryInit
    {
        void Init(DependencyManager dep);
    }

	public abstract class BehaviorFactory : IBehaviorFactoryInit, IArrange {

        private DependencyManager Dependencies;

        public IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr)
        {
            throw new NotImplementedException();
        }

        public IBehavior Arrange<TMocked>(Expression<Action<TMocked>> expr)
        {
            throw new NotImplementedException();
        }

        public IBehavior<Y> Arrange<Y>(IBehavior<Y> method, Action<Y> action) where Y : class
        {
            throw new NotImplementedException();
        }

        public IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Y returnValue) { throw new NotImplementedException(); }

        void IBehaviorFactoryInit.Init(DependencyManager dep)
        {
            Dependencies = dep;
        }

        public T Any<T>() { throw new NotImplementedException(); }

        public abstract void Setup();


    }
}
