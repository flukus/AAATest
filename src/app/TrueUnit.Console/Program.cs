using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrueUnit.ExampleTests;
using TrueUnit.Framework;

namespace TrueUnit.Console
{
	class Program
	{
		static void Main(string[] args)
		{

			var extractor = new TestExtractor();
			var tests = extractor.ExtractTests(new Assembly[] { typeof(UserControllerTest).Assembly });
			var runner = new TestRunner();
			runner.Execute(tests);

		}
	}
}
