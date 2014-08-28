using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.ExampleProj;
using AAATest.ExampleProj.Dependencies;

namespace TrueUnit.ExampleTests {
	public class ProductController_View : Test<ProductController> {

		public void ExceptionWhenId0() {
			Act(x => x.View(0));
			AssertException<ArgumentException>("id must be provided. Provided value was: '0'");
		}

		public void ExceptionWhenIdNegative() {
			Act(x => x.View(-2893));
			AssertException<ArgumentException>("id must be provided. Provided value was: '-2893'");
		}

		public void ProductLoadedFromRepository() {
			Arrange((IRepository x) => x.GetById<Product>(27))
				.Returns(new Product())
				.Verifiable();
			Act(x => x.View(27));
			Assert();
		}

		public void ExceptionWhenUnknownId() {
			Arrange((IRepository x) => x.GetById<Product>(99))
				.Returns<Product>(null);
			Act(x => x.View(99));
			AssertException("Unable to find product with id: '99'");
		}

	}
}
