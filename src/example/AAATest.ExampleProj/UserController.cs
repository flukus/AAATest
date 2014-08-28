using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.ExampleProj.Dependencies;

namespace AAATest.ExampleProj
{
  public class UserController
  {
	private readonly ISession Session; 

	public UserController(ISession session)
	{
	  Session = session;
	}

	public ViewResult View(Guid id)
	{
	  var user = Session.GetById<User>(id);
	  var result = new UserViewVM { UserId = user.Id, UserName = user.Name };
	  return View(result);
	}

	private ViewResult View()
	{
	  return new ViewResult();
	}

	private ViewResult View(object dataItem)
	{
	  return new ViewResult() { DataItem = dataItem };
	}
  }
}
