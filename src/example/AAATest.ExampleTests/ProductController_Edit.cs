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

		public void CurrentUserFromSession() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product { ManagedBy = new User { } });
			Arrange((ISession x) => x.GetCurrentUser())
				.Returns(new UserProfile())
				.Verifiable();
			Act(x => x.Edit(98));
			Assert();
		}

		public void ExceptionWhenCurrentUserNotProductOwner() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product() { ManagedBy = new User { Id = 27 } });
			Arrange((ISession x) => x.GetCurrentUser()).Returns(new UserProfile() { UserId = 23 });
			Act(x => x.Edit(99));
			AssertException("You do not have permission to edit that product");
		}

		public void ResultFromReturnedObject() {
			Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
				.Returns(new Product { Id = 76, Name = "Super Awesome Gizmo", ManagedBy = new User() });
			Arrange((ISession x) => x.GetCurrentUser()).Returns(new UserProfile());
			Act(x => x.Edit(76));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Equal(x => x.ProductName, "Super Awesome Gizmo")
				.Equal(x => x.ProductId, 76);
		}

		public void AvoidsLazyLoadingProductManager() {
			//TODO: need syntax to handle new mock
			Arrange((IRepository x) => x.Query<Product>())
				.Returns(GetMocked<IQuery<Product>>());
			Arrange((IQuery<Product> x) => x.Include<User>(It.IsAny<Func<Product, User>>()))
				.Returns(GetMocked<IQuery<Product>>()).Verifiable();
			Arrange((IQuery<Product> x) => x.Where(It.IsAny<Func<Product, bool>>()))
				.Returns(GetMocked<IQuery<Product>>());
			Arrange((IQuery<Product> x) => x.First())
				.Returns(new Product { ManagedBy = new User { } });
			Act(x => x.Edit(31));
			Assert();
		}

	}
}
