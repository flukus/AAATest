using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.ClassesToTest.Dependencies
{

  public interface IServiceLocator {
	T GetService<T>() where T : class;
  }

  public class DefaultServiceLocator : IServiceLocator
  {
	public T GetService<T>() where T : class
	{
	  return default(T);
	}
  }

  public static class ServiceLocator
  {

	public static IServiceLocator Locator = new DefaultServiceLocator();

	public static T GetService<T>() where T : class
	{
	  return Locator.GetService<T>();
	}
  }
}
