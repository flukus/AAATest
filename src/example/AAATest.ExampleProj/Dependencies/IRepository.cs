using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleProj.Dependencies {
	public interface IRepository {
		T GetById<T>(int id);
	}
}
