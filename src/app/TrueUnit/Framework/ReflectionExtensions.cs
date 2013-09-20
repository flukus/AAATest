using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public static class ReflectionExtensions
	{
		public static bool IsTest(this Type type)
		{
			return typeof(ITest).IsAssignableFrom(type);
		}

		public static bool IsTestMethod(this MethodInfo method)
		{
		 return method.IsPublic && method.Name.StartsWith("Test");
		}

		public static T As<T>(this object obj)
			where T : class
		{
			T t = obj as T;
			if (t == null)
				throw new Exception(string.Format("Could not convert type '{0}' to type '{1}'", obj.GetType().Name, typeof(T).Name));
			return t;
		}
	}
}
