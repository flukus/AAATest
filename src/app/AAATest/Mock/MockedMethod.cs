using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{
    public class MockedMethod : IMethodStub
    {
        public int CallCount { get; set; }
        public object ReturnValue { get; set; }

        public List<Matcher> Matchers { get; set; }

        internal bool IsMatch(object[] p)
        {
            for (int i = 0; i < p.Length; i++)
            {
                var matcher = Matchers[i];
                if (!matcher.IsMatch(p[i]))
                    return false;
            }
            return true;
        }
    }
}
