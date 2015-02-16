using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class MockProxy<T>
    {
        T Object { get; set; }
        MockInterceptor Interceptor { get; set; }
    }
}
