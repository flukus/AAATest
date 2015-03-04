using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework.Exceptions;

namespace AAATest.Test {
	public class TestFixture_AssertTests : TestFixture<TestFixture<object>> {
		public void AsserFailsAfterException() {
			Arrange(x => x.Act(y=> {throw new Exception("foo"); }));
			Act(x => x.Assert());
			AssertException<AssertException>("Cannot assert because act threw exception");
		}

	}
}
