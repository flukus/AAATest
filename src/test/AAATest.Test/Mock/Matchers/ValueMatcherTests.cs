using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Mock;

namespace AAATest.Test.Mock.Matchers {
	public class ValueMatcherTests : TestFixture<ValueMatcher> {
		public void TrueIfBothNull() {
			Arrange(x => x.ExpectedValue = null);
			Act(x => x.IsMatch(null));
			Assert().IsTrue();
		}

		public void TrueIfBothEqual() {
			Arrange(x => x.ExpectedValue = 6);
			Act(x => x.IsMatch(6));
			Assert().IsTrue();
		}

		public void FalseIfExpectedNullAndRecievedValue() {
			Arrange(x => x.ExpectedValue = null);
			Act(x => x.IsMatch("nvrnvinci"));
			Assert().IsFalse();
		}

		public void FalseIfExpectedValueAndReccievedValue() {
			Arrange(x => x.ExpectedValue = "nrieneif");
			Act(x => x.IsMatch("nvrnvinci"));
			Assert().IsFalse();
		}

		public void TrueForReferenceMatch() {
			var obj = new Object();
			Arrange(x => x.ExpectedValue = obj);
			Act(x => x.IsMatch(obj));
			Assert().IsTrue();
		}

	}
}
