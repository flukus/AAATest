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
	public class CommonBehaviors
	{
        public readonly UserProfile User;

        public CommonBehaviors() {
            User = new UserProfile();
        }
		
	}

}
