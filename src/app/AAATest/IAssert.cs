using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Mock;

namespace AAATest {
	public interface IAssert {

		void Assert(IBehavior behavior, int count);
	}
}
