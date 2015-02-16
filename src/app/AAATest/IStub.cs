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

    public interface IStubProviderInit
    {
        void Init(DependencyManager dep);
    }

	public abstract class StubProvider : IStubProviderInit, IArrange {

        private DependencyManager Dependencies;

        public IReturns<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr)
        {
            throw new NotImplementedException();
        }

        public IMethodStub Arrange<TMocked>(Expression<Action<TMocked>> expr)
        {
            throw new NotImplementedException();
        }

        public IReturns<Y> Arrange<Y>(IReturns<Y> method, Action<Y> action) where Y : class
        {
            throw new NotImplementedException();
        }

        void IStubProviderInit.Init(DependencyManager dep)
        {
            Dependencies = dep;
        }

        public T Any<T>() { throw new NotImplementedException(); }


    }
}
