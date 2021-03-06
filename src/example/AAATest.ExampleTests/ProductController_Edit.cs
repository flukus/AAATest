﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.ExampleProj;
using AAATest.ExampleProj.Dependencies;
using AAATest.ExampleTests.Stubs;
using System.Linq.Expressions;

namespace AAATest.ExampleTests {

	class ProductController_Edit : TestFixture<ProductController> {

		public void ExceptionWhenId0() {
			Act(x => x.Edit(0));
			AssertException<ArgumentException>("id must be provided. Provided value was: '0'");
		}

		public void ExceptionWhenIdNegative() {
			Act(x => x.Edit(-8922));
			AssertException<ArgumentException>("id must be provided. Provided value was: '-8922'");
		}

		public void ProductLoadedFromRepository() {
			var byId = Arrange((IRepository x) => x.GetById<Product>(27))
					.Returns(new Product { Id = 27, Name = "27" });
			Act(x => x.Edit(27));
			Assert(byId);
		}

		public void ExceptionWhenUnknownId() {
			Arrange((IRepository x) => x.GetById<Product>(99))
				.Returns(null);
			Act(x => x.Edit(99));
			AssertException("Unable to find product with id: '99'");
		}

		public void ResultFromReturnedObject() {
			Arrange((IRepository x) => x.GetById<Product>(Any<int>()))
				.Returns(new Product { Id = 76, Name = "Super Awesome Gizmo" });
			Arrange((ISession x) => x.GetCurrentUser()).Returns(new UserProfile());
			Act(x => x.Edit(76));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Equal(x => x.ProductName, "Super Awesome Gizmo")
				.Equal(x => x.ProductId, 76);
		}

		public void CategoryInfoIsSet() {
			Arrange((IRepository x) => x.GetById<Product>(Any<int>()))
				.Returns(new Product { Category = new Category { Id = 3, Name = "foo" } });
			Act(x => x.Edit(34));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Equal(x => x.CategoryId, 3)
				.Equal(x => x.CategoryName, "foo");
		}

		public void CategoryNullIfUnknown() {
			Arrange((IRepository x) => x.GetById<Product>(Any<int>()))
				.Returns(new Product { Category = null });
			Act(x => x.Edit(34));
			Assert<ViewResult, ProductEditVM>(x => x.DataItem)
				.Null(x => x.CategoryId)
				.Null(x => x.CategoryName);
		}

		/*
		public void AvoidsLazyLoadingCategory() {
			Arrange((IRepository x) => x.Query<Product>())
				.Returns(GetMocked<IQuery<Product>>());
			var incCat = Arrange((IQuery<Product> x) => x.Include<Category>(Any<Expression<Func<Product, Category>>>()))
					.ReturnsSelf();
			Arrange((IQuery<Product> x) => x.Where(Any<Expression<Func<Product, bool>>>()))
				.ReturnsSelf();
			Arrange((IQuery<Product> x) => x.First())
				.Returns(new Product { Category = new Category { } });
			Act(x => x.Edit(31));
			Assert(incCat);

		}
		 */



	}
}
