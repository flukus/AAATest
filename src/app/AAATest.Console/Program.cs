using System;
using System.Linq;
using System.Reflection;
using System.IO;
using AAATest.Framework;

namespace AAATest.Console
{
	class Program {
		static int Main(string[] args)
		{
			var suite = new UnitTestSuite(new ReflectionUtil(), new ConsoleListener());
			System.Console.WriteLine(Environment.CurrentDirectory);
			var path = Path.GetFullPath(args[0]);
			System.Console.WriteLine(path);
			AppDomain.CurrentDomain.AssemblyResolve += MyHandler;
			var ass = Assembly.LoadFile(path);
			var referenced = ass.GetReferencedAssemblies();
			var dirName = Path.GetDirectoryName(path);
			var dirInfo = new DirectoryInfo(dirName);
			foreach (var refAss in referenced){
				var file = dirInfo.GetFiles()
					.FirstOrDefault(x => x.Name.StartsWith(refAss.Name));

				Assembly actualAss;
				if (file != null)
					actualAss = Assembly.LoadFile(file.FullName);
			}
			suite.Init(null, ass);
			bool result = suite.Execute();

			if (result)
				return 0;
			else return -1;

		}

		static Assembly MyHandler(object source, ResolveEventArgs e) {
			if (e.RequestingAssembly == null)
				return null;
			System.Console.WriteLine("Resolving {0}", e.Name);
			string assemblyName = e.Name.Split(',')[0];
			var dirName = e.RequestingAssembly.Location;
			dirName = Path.GetDirectoryName(dirName);
			var dirInfo = new DirectoryInfo(dirName);
			var file = dirInfo.GetFiles()
				.FirstOrDefault(x => x.Name.StartsWith(assemblyName));

			if (file != null)
				return Assembly.LoadFile(file.FullName);
			else return null;
		}
	}
}
