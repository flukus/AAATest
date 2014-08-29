using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework {
	public class TestCompletedInfo {
		public TestResult Result { get; set; }
		public string ErrorName { get; set; }
		public string ErrorMessage { get; set; }
		public string StackTrace { get; set; }
	}

	public enum TestResult {

		/// <summary>
		/// Test completed successfully
		/// </summary>
		Passed = 1,

		/// <summary>
		/// Test completed but the assertion failed
		/// </summary>
		Failed = 2,

		/// <summary>
		/// There was an error during with the test, it was not setup correctly and did not proceed through required stages
		/// </summary>
		Error = 3,

		/// <summary>
		/// There was an error from the framework itself, this will obviously never be used
		/// </summary>
		FrameworkError = 4

	}
}
