using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework.Exceptions;
using AAAUnit.Framework.Exceptions;

namespace AAATest {
	public class Assert {
		private object Result;

		public Assert(object result) {
			Result = result;
		}

		public Assert IsTrue() {
			if (Result == null)
				throw new AssertException("Expected type to be bool but was null");
			if (Result.GetType() != typeof(bool))
				throw new AssertException(string.Format("Expected type to be bool but was '{0}'", Result.GetType()));
			var boolValue = (bool)Result;
			if (!boolValue)
				throw new AssertException("Expected return value to be true but was false");
			return this;
		}

		public Assert IsFalse() {
			if (Result == null)
				throw new AssertException("Expected type to be bool but was null");
			if (Result.GetType() != typeof(bool))
				throw new AssertException(string.Format("Expected type to be bool but was '{0}'", Result.GetType()));
			var boolValue = (bool)Result;
			if (boolValue)
				throw new AssertException("Expected return value to be false but was true");
			return this;
		}
	}

	public class Assert<T> : Assert {
		public T Result;

		public Assert(T result)
			: base(result) {
			// TODO: Complete member initialization
			this.Result = result;
		}

		public Assert<T> Equal<TCompare>(Func<T, TCompare> actual, TCompare expected) {
			object actualObj = actual(Result);
			if (actualObj == null && expected == null)
				throw new AssertException(string.Format("Expected '{0}' but was null", expected.ToString()));
			if (!actualObj.Equals(expected))
				throw new AssertException(string.Format("Expected '{0}' but was {1}", expected.ToString(), actualObj.ToString()));

			return this;
		}

		public Assert<T> Null<TCompare>(Func<T, TCompare> actual) {
			object actualObj = actual(Result);
			if (actualObj != null)
				throw new AssertException(string.Format("Expected null but object has value '{0}'", actual.ToString()));

			return this;
		}

	}
}
