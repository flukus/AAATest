using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.ExampleProj.Dependencies;
using Moq;
using Moq.Language.Flow;
using AAATest;

namespace AAATest.ExampleTests.Stubs {

	class ProductRepository : StubProvider<IQuery<Product>> {

		public Product Product { get; set; }
		public List<Product> Products { get; set; }
		public IReturnsResult<IQuery<Product>> IncludeCategory { get; set; }

		public ProductRepository() {
			Product = new Product();
		}

		public void InitData() {

		}

		protected override void Initialize() {
			/*ArrangeFluent(x => x.Include<Category>());
			Arrange(x => x.First()).Returns(Product);
			var iq = deps.GetMock<IQuery<Product>>();
			iq.Setup().Returns(iq);
			stub.Setup(x => x.Query<Product>())
				.Returns(GetMocked<IQuery<Product>>());
			 * */
			//IncludeCategory = 
			Arrange(x => x.Include<Category>(It.IsAny<Func<Product, Category>>()))
				.Returns(Object);
		}
	}
}
