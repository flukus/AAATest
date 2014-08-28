using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.ExampleProj.Dependencies;

namespace AAATest.ExampleTests.Data
{

	class DefaultUser : User
	{
	}

	public class EmptyUserList : List<User>
	{
	}

	public class DefaultUserList : List<User>
	{
		public void Setup()
		{
			Add(new User { Id = Guid.NewGuid(), Name = "luke" });
			Add(new User { Id = Guid.NewGuid(), Name = "paul" });
		}
	}


}
