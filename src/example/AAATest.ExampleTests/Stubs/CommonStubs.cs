using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.ExampleProj.Dependencies;
using Moq;
using AAATest.ExampleTests.Data;
using AAATest;

namespace AAATest.ExampleTests.Stubs
{
	public class CommonStubs : IStub<ISession>
	{
		public static User DefaultUser = null;

		void IStub<ISession>.Stub(Mock<ISession> stub)
		{
			var dm = new DataManager();
			DefaultUser = new User { Id = Guid.NewGuid(), Name = "frank" };

			//stub.Setup(x => x.Query<User>())
			//.Returns(dm.Get<EmptyUserList>().AsQueryable());

			stub.Setup(x => x.GetById<User>(It.IsAny<Guid>()))
				.Returns(DefaultUser);
		}
	}

	public class DataManager
	{
		public T Get<T>()
		{
			return default(T);
		}
	}

}
