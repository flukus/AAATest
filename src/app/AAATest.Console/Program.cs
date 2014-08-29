using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using AAATest.ExampleTests;

namespace AAATest.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var suite = new UnitTestSuite(new ReflectionUtil(), new StubCollection(), new ConsoleListener());
			suite.Init(null, typeof(UserControllerTest).Assembly);
			suite.Execute();
			System.Console.ReadLine();

		}
	}
}
