using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace AAATest.Framework {
	public interface ITestResultListener {
		void TestStarted(string testName);
		void TestComplete(TestCompletedInfo result);
		void AllTestsComplete();
	}
}
