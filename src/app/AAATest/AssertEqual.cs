using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest
{
	public static class AssertEqual
	{

		public static Assert<T> IsEqual<T, U>(this Assert<T> assert, U expected, Func<T, U> actual)
			where U : IComparable
		{
			var result = actual(assert.Result);
			if (!expected.Equals(result))
				throw new Exception(string.Format("Expected {0} but was {1}", expected, result));
			return assert;
		}

		public static Assert<T> IsEqual<T>(this Assert<T> assert, Guid expected, Func<T, Guid> actual)
		{
			var result = actual(assert.Result);
			if (!expected.Equals(result))
				throw new Exception(string.Format("Expected {0} but was {1}", expected, result));
			return assert;
		}
	}
}
