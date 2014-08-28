using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace AAATest.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			var suite = new UnitTestSuite(new ReflectionUtil(), new StubCollection());
			suite.Init(null, typeof(Program).Assembly);
			suite.Execute();
			System.Console.ReadLine();
		}
	}
}
