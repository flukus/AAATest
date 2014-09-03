using System;
using System.Reflection;
using System.IO;
using AAATest.Framework;

namespace AAATest.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var suite = new UnitTestSuite(new ReflectionUtil(), new StubCollection(), new ConsoleListener());
			System.Console.WriteLine(Environment.CurrentDirectory);
			var path = Path.GetFullPath("build\\example\\ExampleTests.dll");
			System.Console.WriteLine(path );
			var ass = Assembly.LoadFile(path);
			suite.Init(null, ass);
			suite.Execute();

		}
	}
}
