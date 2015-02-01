using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework
{
	public class DependencyManager
	{
		private Dictionary<Type, Moq.Mock> Mocks = new Dictionary<Type, Mock>();

		public DependencyManager()
		{
		}

		public Moq.Mock<T> GetMock<T>() where T : class {
			var type = typeof(T);
			if (Mocks.ContainsKey(type))
				return Mocks[type] as Moq.Mock<T>;
			else {
				var mock = new Moq.Mock<T>();
				Mocks[type] = mock;
				return mock;
			}
		}

		public Mock GetMock(Type t)
		{
			return (Mock)Mocks[t];
		}

		public Mock GetOrCreateMock(Type t)
		{
			if (Mocks.ContainsKey(t))
				return Mocks[t];
			var mockType = typeof(Mock<>).MakeGenericType(t);
			var mock = mockType.GetConstructor(new Type[0]).Invoke(null) as Mock;
			Mocks[t] = mock;
			return mock as Mock;
		}

		public IEnumerable<Mock> AllMocks()
		{
			return Mocks.Values;
		}

		public IEnumerable<Mock> CreateDependencies(IEnumerable<Type> types) {
			var list = new List<Mock>();
			foreach (var type in types) {
				var mock = GetOrCreateMock(type);
				list.Add(mock);
			}
			return list;
		}
	}
}
