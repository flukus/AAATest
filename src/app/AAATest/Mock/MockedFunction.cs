using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class MockedFunction<T> : IBehavior<T>
    {
        public IBehavior<T> Returns(T returnValue)
        {
            throw new NotImplementedException();
        }

        public IBehavior ReturnsSelf()
        {
            throw new NotImplementedException();
        }
    }
}
