using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueUnit.Framework
{
	public interface ITest
	{
		void Initialize(TestHelper dependencyManager);
	}
}
