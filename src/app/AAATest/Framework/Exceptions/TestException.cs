using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAUnit.Framework.Exceptions {
	public class TestException : Exception {
		public TestException(string message) : base(message) { }
		public TestException(string message, Exception innerException) : base(message, innerException) { }
	}
}
