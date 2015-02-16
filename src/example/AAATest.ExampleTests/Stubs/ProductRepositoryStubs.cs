﻿using System;
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

	class ProductRepository : StubProvider {

        public readonly Product Default;
        //public readonly Product Product;
        public readonly List<Product> Products;

        public IMethodStub IncludeCategory;
        public IReturns<Product> GetById;
        public IReturns<Product> FirstOrDefault;

		public ProductRepository() {
            Default = new Product { Id = 23, Name = "hello", Category = new Category { Id = 4, Name = "Some Category" } };
            Products = new List<Product> {
                Default,
                new Product { Id = 45, Name = "Product with really long name"},
                new Product { Id = 6789, Name = "IPad"}
            };
		}

		public void Setup() {
            IncludeCategory = Arrange((IQuery<Product> x) => x.Include(Any<Expression<Func<Product, Category>>>()))
                .ReturnsSelf();
            GetById = Arrange((IRepository x) => x.GetById<Product>(Any<int>()))
                .Returns(Default);

            FirstOrDefault = Arrange((IQuery<Product> q) => q.First()).Returns(Default);
		}
	}
}