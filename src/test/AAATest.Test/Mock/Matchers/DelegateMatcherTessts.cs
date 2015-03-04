using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Mock;

namespace AAATest.Test.Mock.Matchers {
	public class DelegateMatcherTessts : TestFixture<DelegateMatcher> {
		public void IsMatchIfExpressionTrue() {
			Act(x => x.Delegate = new Func<object, bool>(y => { return true; }));
			Act(x => x.IsMatch(null));
			Assert().IsTrue();
		}

		public void IsNotMatchIfExpressionFalse() {
			Act(x => x.Delegate = new Func<object, bool>(y => { return false; }));
			Act(x => x.IsMatch(null));
			Assert().IsFalse();
		}
	}
}
