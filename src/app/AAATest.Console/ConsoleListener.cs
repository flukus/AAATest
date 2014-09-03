using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace AAATest.Console {
	class ConsoleListener : ITestResultListener {

		private int TotalCount = 0;
		private int PassedCount = 0;

		public void TestStarted(string testName) {
			System.Console.Write(testName);
			System.Console.Write("...");
			TotalCount++;
		}

		public void TestComplete(AAATest.Framework.TestCompletedInfo result) {
			if (result.Result == TestResult.Passed) {
				System.Console.WriteLine("    Passed");
				PassedCount++;
			} else if (result.Result == Framework.TestResult.Failed) {
				System.Console.WriteLine("    FAILED:");
				System.Console.WriteLine("{0}: {1}", "Assert", result.ErrorMessage);
				System.Console.WriteLine(result.StackTrace);
				System.Console.WriteLine("");
				System.Console.WriteLine("");
			} else {
				//framework error, include full stack trace
				System.Console.WriteLine("    Framework Error:");
					System.Console.WriteLine("{0}: {1}", result.ErrorName, result.ErrorMessage);
					System.Console.WriteLine(result.StackTrace);
					System.Console.WriteLine("");
					System.Console.WriteLine("");
			}
		}

		public void AllTestsComplete() {
			System.Console.WriteLine("");
			System.Console.WriteLine("{0} tests executed. {1} Passed, {2} Failed", TotalCount, PassedCount, TotalCount - PassedCount);
		}
	}
}
