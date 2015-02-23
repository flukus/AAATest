using AAATest.Mock;
using System;
using System.Linq.Expressions;
namespace AAATest
{
    interface IArrange
    {
        IBehavior<TReturn> Arrange<TMocked, TReturn>(Expression<Func<TMocked, TReturn>> expr) where TMocked : class;
        IBehavior Arrange<TMocked>(Expression<Action<TMocked>> expr);
        //Moq.Mock<Y> Arrange<Y>() where Y : class;
        IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Action<Y> action) where Y : class;
        IBehavior<Y> Arrange<Y>(IBehavior<Y> behavior, Y returnValue);
    }
}
