using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock {

	public class Matcher {
		public virtual bool IsMatch(object value) {
			return true;
		}
	}

	public class ValueMatcher : Matcher {
		public object ExpectedValue { get; set; }

		public override bool IsMatch(object value) {
			if (ExpectedValue == null && value == null)
				return true;
			if (ExpectedValue == null || value == null)
				return false;
			return ExpectedValue.Equals(value);
		}
	}

	public class DelegateMatcher : Matcher {
		public Delegate Delegate;

		public override bool IsMatch(object value) {
			object result = Delegate.DynamicInvoke(value);
			return (bool)result;
		}
	}

}
