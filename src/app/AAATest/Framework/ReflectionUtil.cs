using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Framework
{
	public class ReflectionUtil {

		public ConstructorInfo GetCtor(Type type) {
			var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
				.OrderByDescending(x => x.GetParameters().Count())
				.FirstOrDefault();
			return ctor;
		}

		//TODO: Clean this up a bit, it's very tightly coupled
		public IList<Tuple<Type, Type, MethodInfo>> FindGenericImplimentationByName(IEnumerable<Type> allTypes, string name) {
			var stubMethods = new List<Tuple<Type, Type, MethodInfo>>();
			foreach (var type in allTypes) {
				var stubInterfaces = type.GetInterfaces()
					.Where(x => x.Name.StartsWith(name) && x.GetGenericArguments().Count() == 1);
				foreach (var stubInterface in stubInterfaces) {
					var stubType = stubInterface.GetGenericArguments().First();
					var map = type.GetInterfaceMap(stubInterface);
					var method = map.InterfaceMethods.FirstOrDefault();
					stubMethods.Add(new Tuple<Type, Type, MethodInfo>(type, stubType, method));
				}
			}
			return stubMethods;
		}


		public IEnumerable<Type> FindClassesByBaseType(IEnumerable<Type> allTypes, Type baseType) {
			var types = allTypes.Where(x => x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == baseType);
			return types;
		}

		public Type GetGenericParameterOfBaseType(Type type, Type baseType) {
			return type.BaseType.GetGenericArguments()[0];
		}

		public IEnumerable<MethodInfo> GetPublicMethods(Type type) {
			return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
		}

		public object CreateFromEmptyConstructor(Type type) {
			var ctor = type.GetConstructor(new Type[0]);
			object obj = ctor.Invoke(null);
			return obj;
		}

		public IEnumerable<Type> GetCtorParameters(Type type) {
			var ctor = GetCtor(type);
			return ctor.GetParameters().Select(x => x.ParameterType);
		}

		public object CreateTypeWithArguments(Type type, params object[] parameters) {
			var ctor = GetCtor(type);
			var obj = ctor.Invoke(parameters);
			return obj;
		}

		public void InvokeMethod(object obj, MethodInfo method, params object[] parameters) {
			method.Invoke(obj, parameters);
		}

		public Type CreateGenericType(Type type, Type genericParameter) {
			var newType = type.MakeGenericType(genericParameter);
			return newType;
		}
	}
}
