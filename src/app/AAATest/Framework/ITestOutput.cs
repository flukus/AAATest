using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace TrueUnit.Framework {
	interface ITestOutput {
		void TestStarted(string testName);
		void TestFinished(TestResult result);
	}
}
