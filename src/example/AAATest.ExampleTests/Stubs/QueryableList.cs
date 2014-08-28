using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleTests.Stubs
{
  public class QueryableList<T> : IQueryable<T> 
  {
	public readonly List<T> List;

	private readonly IQueryProvider QueryProvider;

	public QueryableList()
	{
	  QueryProvider = new QueryProvider<T>(this);
	  List = new List<T>();
	}

	public QueryableList(IEnumerable<T> source)
	{
	  List.AddRange(source);
	}

	public void Add(T t)
	{
	  List.Add(t);
	}

	#region IQueryable<T> implementation
	public Expression Expression
	{
	  get { return List.ToArray().AsQueryable().Expression; }
	}

	public Type ElementType
	{
	  get { return typeof(T); }
	}

	public IQueryProvider Provider
	{
	  get { return QueryProvider; }
	}
	#endregion



	public IEnumerator<T> GetEnumerator()
	{
	  return List.GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
	  return List.GetEnumerator();
	}
  }

  public class QueryProvider<T> : IQueryProvider
  {
	QueryableList<T> List;
	public QueryProvider(QueryableList<T> list)
	{
	  List = list;
	}
	public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
	{
	  var c = expression as MethodCallExpression;
	  throw new NotImplementedException();
	}

	public IQueryable CreateQuery(Expression expression)
	{
	  throw new NotImplementedException();
	}

	public TResult Execute<TResult>(Expression expression)
	{
	  throw new NotImplementedException();
	}

	public object Execute(Expression expression)
	{
	  throw new NotImplementedException();
	}
  }
}
