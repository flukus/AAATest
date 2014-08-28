using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.ExampleProj.Dependencies;
using Moq;
using AAATest;
using AAATest.ExampleProj;

namespace AAATest.ExampleTests
{
	public class UserControllerTest :
		Test<UserController>
	{
		public void TestViewLoadsDataFromISession()
		{
			var id = Guid.NewGuid();
			Arrange((ISession x)=> x.GetCurrentUser()).Returns(new User()).Verifiable();
			Act(x => x.View(id));
			Assert();
		}

		public void TestViewWithStubsReturnsViewResult()
		{
			Act(x => x.View(Guid.NewGuid()));
			Assert<ViewResult>();
		}

		public void TestViewReturnsViewModel()
		{
			var user = new User() { Id = Guid.NewGuid(), Name = "frank" };
			var setup = Arrange<ISession, User>(x => x.GetById<User>(user.Id));
			setup.Returns(user);
			Act(x => x.View(user.Id));
			Assert<ViewResult, UserViewVM>(x => x.DataItem)
			.IsEqual(user.Id, x => x.UserId)
			.IsEqual(user.Name, x => x.UserName);
		}

		public void TestExceptionIfNoUser()
		{
			Arrange<ISession>().Setup(x => x.GetById<User>(It.IsAny<Guid>())).Throws(new Exception("Could not find user")).Verifiable();
			Act(x => x.View(Guid.NewGuid()));
			AssertException("Could not find user");
		}
	}
}
