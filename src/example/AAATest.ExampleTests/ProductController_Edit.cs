using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.ExampleProj;
using AAATest.ExampleProj.Dependencies;
using Moq;

namespace AAATest.ExampleTests {
	public class ProductController_Edit : TestFixture<ProductController> {

		public void ExceptionWhenId0() {
			Act(x => x.Edit(0));
			AssertException<ArgumentException>("id must be provided. Provided value was: '0'");
		}

		public void ExceptionWhenIdNegative() {
			Act(x => x.Edit(-8922));
			AssertException<ArgumentException>("id must be provided. Provided value was: '-8922'");
		}

		public void ProductLoadedFromRepository() {
			Arrange((IRepository x) => x.GetById<Product>(27))
				.Returns(new Product())
				.Verifiable();
			Act(x => x.Edit(27));
		}

		public void ExceptionWhenUnknownId() {
			Arrange((IRepository x) => x.GetById<Product>(99))
				.Returns<Product>(null);
			Act(x => x.Edit(99));
			AssertException("Unable to find product with id: '99'");
		}

		public void ResultFromReturnedObject() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product { Id = 76, Name = "Super Awesome Gizmo" });
			Arrange((ISession x) => x.GetCurrentUser()).Returns(new UserProfile());
			Act(x => x.Edit(76));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Equal(x => x.ProductName, "Super Awesome Gizmo")
				.Equal(x => x.ProductId, 76);
		}

		public void CategoryInfoIsSet() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product { Category = new Category { Id = 3, Name = "foo" } });
			Act(x => x.Edit(34));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Equal(x => x.CategoryId, 3)
				.Equal(x => x.CategoryName, "foo");
		}

		public void CategoryNullIfUnknown() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product { Category = null });
			Act(x => x.Edit(34));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Null(x => x.CategoryId)
				.Null(x => x.CategoryName);
		}


		public void AvoidsLazyLoadingCategory() {
			//TODO: need syntax to handle new mock
			Arrange((IRepository x) => x.Query<Product>())
				.Returns(GetMocked<IQuery<Product>>());
			Arrange((IQuery<Product> x) => x.Include<Category>(It.IsAny<Func<Product, Category>>()))
				.Returns(GetMocked<IQuery<Product>>()).Verifiable();
			Arrange((IQuery<Product> x) => x.Where(It.IsAny<Func<Product, bool>>()))
				.Returns(GetMocked<IQuery<Product>>());
			Arrange((IQuery<Product> x) => x.First())
				.Returns(new Product { Category = new Category { } });
			Act(x => x.Edit(31));
			Assert();
		}

	}
}
