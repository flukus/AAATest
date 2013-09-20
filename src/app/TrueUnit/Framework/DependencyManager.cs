using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public class DependencyManager
	{
		private Dictionary<Type, Mock> Mocks = new Dictionary<Type, Mock>();
		private UnitTest UnitTest;

		public DependencyManager(UnitTest test)
		{
			UnitTest = test;
		}

		public Mock<T> GetMock<T>() where T : class
		{
			var type = typeof(T);
			if (Mocks[type] != null)
				return Mocks[type] as Mock<T>;
			else
			{
				var mock = new Mock<T>();
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
			ApplyStubs(mock, t);
			return mock as Mock;
		}

		public object CreateUnit(Type type)
		{
			var unitConstructor = type.GetConstructors()
				.OrderByDescending(x => x.GetParameters().Length)
				.First();

			var parameters = new object[unitConstructor.GetParameters().Length];
			var parameterTypes = unitConstructor.GetParameters().Select(x => x.ParameterType).ToList();
			for (int i = 0; i < parameters.Length; i++)
			{
				var paramType = parameterTypes[i];
				if (paramType.IsInterface)
				{
					var mock = GetOrCreateMock(paramType);
					parameters[i] = mock.Object;
				}
			}

			var unit = unitConstructor.Invoke(parameters);
			return unit;
		}

		public void ApplyStubs(Mock mock, Type mockType)
		{
			foreach (var stub in UnitTest.DefaultStubs.Where(x=>x.StubType == mockType))
			{
				var stubber = stub.Constructor.Invoke(null);
				stub.Method.Invoke(stubber, new object[] { mock });
			}
			foreach (var stub in UnitTest.Stubs.Where(x=>x.StubType == mockType))
			{
				var stubber = stub.Constructor.Invoke(null);
				stub.Method.Invoke(stubber, new object[] { mock });
			}
		}

	}
}
