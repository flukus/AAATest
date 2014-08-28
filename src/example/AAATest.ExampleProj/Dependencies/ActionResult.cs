using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.ExampleProj.Dependencies {
	public class ActionResult {
	}

	public class ViewResult : ActionResult {
		public object DataItem { get; set; }

		public ViewResult() {

		}

		public ViewResult(object dataItem) {
			DataItem = dataItem;
		}
	}
}
