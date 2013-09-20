using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueUnit.ClassesToTest;
using TrueUnit.ClassesToTest.Dependencies;

namespace TrueUnit.ExampleTests
{
	public class UserControllerTest :
		Test<UserController>,
		ILocator<ServiceLocatorAdapter>
	{
		public void TestViewReturnsViewResult()
		{
			var id = Guid.NewGuid();
			Arrange<ISession>().Setup(x => x.GetById<User>(id))
			.Returns(new User());
			Act(x => x.View(id));
			Assert<ViewResult>();
		}

		public void TestViewReturnsViewModel()
		{
			var user = new User() { Id = Guid.NewGuid(), Name = "frank" };
			Arrange<ISession, User>(x => x.GetById<User>(user.Id))
			.Returns(user);
			Act(x => x.View(user.Id));
			Assert<ViewResult, UserViewVM>(x => x.DataItem)
			.IsEqual(user.Id, x => x.UserId)
			.IsEqual(user.Name, x => x.UserName);
		}
	}
}
