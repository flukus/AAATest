﻿using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.VisualStudioAdpater
{
  [DefaultExecutorUri(TestExecutor.ExecutorUri)]
  [FileExtension(".dll")]
  [FileExtension(".exe")]
  public class TestDiscoverer : ITestDiscoverer
  {
	public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
	{
	  throw new NotImplementedException();
	}
  }
}
