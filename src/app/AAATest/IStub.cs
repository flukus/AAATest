using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest
{
	public interface IStub<T> where T : class {
		void Stub(Mock<T> stub);
	}
}
