using AAATest.Mock;
using System;
using System.Linq.Expressions;
namespace AAATest
{
    interface IArrange
    {
        IReturns<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr);
        IMethodStub Arrange<TMocked>(Expression<Action<TMocked>> expr);
        //Moq.Mock<Y> Arrange<Y>() where Y : class;
        IReturns<Y> Arrange<Y>(IReturns<Y> method, Action<Y> action) where Y : class;
    }
}
