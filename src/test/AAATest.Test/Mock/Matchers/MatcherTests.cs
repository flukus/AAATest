using System;
using System.Collections.Generic;
using System.Linq;
using AAATest.Mock;

namespace AAATest.Test.Mock.Matchers {
	public class MatcherTests : TestFixture<Matcher> {
		public void ReturnsTrueWithValue() {
			Act(x => x.IsMatch(new object()));
			Assert().IsTrue();
		}
	}
}
