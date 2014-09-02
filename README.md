AAATest
========

AAATest is a (pure) unit test framework that aims to take the tedium out of testing.

It is built with the arrange, act, assert pattern front and center to make tests more self documenting.

It is built with dependency injection in mind, dependency and moq management make tests simple to create.

It produces more readable, more maintainable tests with less code*.

It is not suiteable for integration tests, integration testing has developed in it's own direction over the years and there are much better tools (SpecFlow).


Quick Example
-------------

These are complete tests, no previous setup was required:

    public class ProductController_View : TestFixture<ProductController> {

        public void ExceptionWhenId0() {
            Act(x => x.View(0));
            AssertException<ArgumentException>("id must be provided. Provided value was: '0'");
        }

        public void ResultFromReturnedObject() {
            Arrange((IRepository x) => x.GetById<Product>(It.IsAny<int>()))
                .Returns(new Product { Id = 76, Name = "Super Awesome Gizmo" });
            Act(x => x.View(76));
            Assert<ViewResult, ProductViewVM>(x => x.DataItem)
                .Equal(x => x.ProductName, "Super Awesome Gizmo")
                .Equal(x => x.ProductId, 76);
        }
    }


Tutorial
--------

For a more complete example, read through the [TDD with AAATest tutorial](wiki/01---Welcome-to-TDD)




*Can't actually back this up yet.


