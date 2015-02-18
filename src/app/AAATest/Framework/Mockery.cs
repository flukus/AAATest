using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework
{
	public class Mockery
	{
		private Dictionary<Type, object> Mocks = new Dictionary<Type, object>();
        private static ProxyGenerator ProxyGenerator = new ProxyGenerator();
        private readonly IInterceptor Interceptor;

		public Mockery(IInterceptor interceptor)
		{
            Interceptor = interceptor;
		}


		public object[] GetMocks(Type[] types) {
            var objects = new object[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                objects[i] = GetMock(types[i]);
            }
            return objects;
		}

		public T GetMock<T>() where T : class {
            if (Mocks.ContainsKey(typeof(T)))
                return (T)Mocks[typeof(T)];
            T proxy = null;
            if (typeof(T).IsInterface)
                proxy = ProxyGenerator.CreateInterfaceProxyWithoutTarget<T>(Interceptor);
            Mocks.Add(typeof(T), proxy);
            return proxy;
		}

		public object GetMock(Type type)
		{
            if (Mocks.ContainsKey(type))
                return Mocks[type];
            var proxy = ProxyGenerator.CreateInterfaceProxyWithoutTarget(type, Interceptor);
            Mocks.Add(type, proxy);
            return proxy;
		}

		public IEnumerable<object> AllMocks()
		{
            throw new NotImplementedException();
		}

	}
}
