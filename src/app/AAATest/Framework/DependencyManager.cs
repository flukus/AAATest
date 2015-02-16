using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework
{
	public class DependencyManager
	{
		private Dictionary<Type, MockedProxy> Mocks = new Dictionary<Type, MockedProxy>();

		public DependencyManager()
		{
		}

		public MockedProxy GetMock<T>() where T : class {
            throw new NotImplementedException();
		}

		public MockedProxy GetMock(Type t)
		{
            throw new NotImplementedException();
		}

		public MockedProxy GetOrCreateMock(Type t)
		{
            throw new NotImplementedException();
		}

		public IEnumerable<MockedProxy> AllMocks()
		{
            throw new NotImplementedException();
		}

		public IEnumerable<MockedProxy> CreateDependencies(IEnumerable<Type> types) {
            throw new NotImplementedException();
		}
	}
}
