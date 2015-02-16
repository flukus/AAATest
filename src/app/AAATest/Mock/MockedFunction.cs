using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class MockedFunction<T> : IReturns<T>
    {
        public IReturns<T> Returns(T returnValue)
        {
            throw new NotImplementedException();
        }

        public IMethodStub ReturnsSelf()
        {
            throw new NotImplementedException();
        }
    }
}
