using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock
{

    public class Matcher
    {
        public virtual bool IsMatch(object value)
        {
            return true;
        }
    }

    public class ValueMatcher : Matcher
    {
        private readonly object ExpectedValue;

        public ValueMatcher(object expected)
        {
            ExpectedValue = expected;
        }

        public override bool IsMatch(object value)
        {
            return ExpectedValue.Equals(value);
        }
    }

    class DelegateMatcher : Matcher
    {
        private readonly Delegate Delegate;

        public DelegateMatcher(Delegate d)
        {
            Delegate = d;
        }

        public override bool IsMatch(object value)
        {
            object result = Delegate.DynamicInvoke(value);
            return (bool)result;
        }
    }

}
