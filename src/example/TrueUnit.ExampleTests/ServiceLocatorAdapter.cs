using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueUnit.ClassesToTest.Dependencies;
using TrueUnit.Framework;

namespace TrueUnit.ExampleTests
{
  public class ServiceLocatorAdapter : IServiceLocatorAdapter, IServiceLocator
  {
	DependencyManager Mocks { get; set; }

	public void Register(DependencyManager mocks)
	{
	  Mocks = mocks;
	  ServiceLocator.Locator = this;
	}

	public T GetService<T>() where T : class
	{
	  return Mocks.GetMock<T>().Object;
	}
  }
}
