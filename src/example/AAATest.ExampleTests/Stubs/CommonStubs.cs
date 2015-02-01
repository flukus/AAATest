using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AAATest.ExampleProj.Dependencies;
using Moq;
using AAATest;
using AAATest.Framework;

namespace AAATest.ExampleTests.Stubs
{
	public interface IStubGlobally<T> { }
	public class CommonStubs : 
		IStubGlobally<ISession>
	{
		//void IStub<ISession>.Stub(Mock<ISession> stub, DependencyManager deps)
		//{
			//var iq = deps.GetMock<IQuery<Product>>();
			//stub.Setup(x => x.GetById<User>(It.IsAny<Guid>()))
				//.Returns(iq);

		//}

		//void IStub<ISession>.Stub(Mock<IRepository> stub, DependencyManager deps) {
			//var iq = deps.GetMock<IQuery<Product>>();
			//iq.Setup(x => x.Include<Category>()).Returns(iq);
			//stub.Setup(x => x.Query<Product>())
				//.Returns(GetMocked<IQuery<Product>>());
		//}
	}

}
