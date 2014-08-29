using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework.Exceptions;

namespace AAATest
{
	public class Assert<T>
	{
		public T Result;

		public Assert(T result)
		{
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

	}
}
