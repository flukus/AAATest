using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleProj.Dependencies {
	public interface IRepository {
		T GetById<T>(int id);
		IQuery<T> Query<T>();
	}

	public interface IQuery<T> {
		IQuery<T> Include<Y>(Expression<Func<T, Y>> func);
		IQuery<T> Where(Expression<Func<T, bool>> func);
		T First();
	}
}
