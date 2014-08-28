using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.ExampleProj.Dependencies;

namespace AAATest.ExampleProj {
	public class ProductController {

		private readonly IRepository Repository;
		private readonly ISession Session;

		public ProductController(IRepository repository, ISession session){
			Repository = repository;
			Session = session;
		}

		public ActionResult View(int id) {
			if (id <= 0)
				throw new ArgumentException(string.Format("id must be provided. Provided value was: '{0}'", id));

			var product = Repository.GetById<Product>(id);
			if (product == null)
				throw new Exception(string.Format("Unable to find product with id: '{0}'", id));

			var vm = new ProductViewVM() {
				ProductId = product.Id,
				ProductName = product.Name
			};

			return new ViewResult(vm);
		}

		public ActionResult List() {
			return null;
		}

		public ActionResult Edit() {
			return null;
		}
	}
}
