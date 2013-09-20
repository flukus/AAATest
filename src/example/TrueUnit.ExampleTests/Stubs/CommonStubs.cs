using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueUnit.ClassesToTest.Dependencies;
using Moq;

namespace TrueUnit.ExampleTests.Stubs
{
	public class CommonStubs : IStub<ISession>
	{
		void IStub<ISession>.Stub(Mock<ISession> stub)
		{
			stub.Setup(x => x.Query<User>())
			.Returns(new List<User>().AsQueryable());
		}
	}
}
