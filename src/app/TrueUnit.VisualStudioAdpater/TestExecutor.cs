using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.VisualStudioAdpater
{
  public class TestExecutor : ITestExecutor
  {

	public const string ExecutorUri = "executor://TrueUnit.VisualStudioAdapter";

	public void Cancel()
	{
	  throw new NotImplementedException();
	}

	public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
	{
	  throw new NotImplementedException();
	}

	public void RunTests(IEnumerable<Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
	{
	  throw new NotImplementedException();
	}
  }
}
