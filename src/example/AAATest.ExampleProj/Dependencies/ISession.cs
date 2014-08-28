using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleProj.Dependencies
{
  public interface ISession
  {
		//IQueryable<T> Query<T>();
		T GetById<T>(Guid id);
		User GetCurrentUser();
  }

  public class TestQueryable<T> : IQueryable<T>
  {
	public IEnumerator<T> GetEnumerator()
	{
	  throw new NotImplementedException();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
	  throw new NotImplementedException();
	}

	public Type ElementType
	{
	  get { throw new NotImplementedException(); }
	}

	public System.Linq.Expressions.Expression Expression
	{
	  get { throw new NotImplementedException(); }
	}

	public IQueryProvider Provider
	{
	  get { throw new NotImplementedException(); }
	}
  }

}
