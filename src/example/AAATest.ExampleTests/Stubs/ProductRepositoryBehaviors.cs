using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest;
using AAATest.ExampleProj.Dependencies;
using AAATest;
using System.Linq.Expressions;
using AAATest.Mock;

namespace AAATest.ExampleTests.Stubs {

	class ProductRepository : BehaviorFactory {

		public Product Default;
		//public readonly Product Product;
		public List<Product> Products;

		public IBehavior IncludeCategory;
		//public IBehavior<Product> GetById;
		public IBehavior<Product> FirstOrDefault;

		public override void Setup() {
			Default = new Product { Id = 23, Name = "hello", Category = new Category { Id = 4, Name = "Some Category" } };
			Products = new List<Product> {
                Default,
                new Product { Id = 45, Name = "Product with really long name"},
                new Product { Id = 6789, Name = "IPad"}
            };

			Arrange((IRepository r) => r.Query<Product>())
				//.Returns(base.Mock < IQuery<Product>>());
				.Returns<IQuery<Product>>();
			IncludeCategory = Arrange((IQuery<Product> x) => x.Include(Any<Expression<Func<Product, Category>>>()))
					.ReturnsSelf();
			IncludeCategory = Arrange((IQuery<Product> x) => x.Where(Any<Expression<Func<Product, bool>>>()))
					.ReturnsSelf();
			//GetById = Arrange((IRepository x) => x.GetById<Product>(Any<int>()))
					//.Returns(Default);

			FirstOrDefault = Arrange((IQuery<Product> q) => q.First()).Returns(Default);
		}
	}
}
