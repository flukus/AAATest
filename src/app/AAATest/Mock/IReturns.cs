using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AAATest.Mock
{
    public interface IReturns<T> : IMethodStub
    {
        IReturns<T> Returns(T returnValue);
        IMethodStub ReturnsSelf();
    }

}
