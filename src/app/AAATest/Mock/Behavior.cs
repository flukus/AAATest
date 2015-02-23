using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAATest.Mock {
	public class Behavior : IBehavior {
		public int CallCount { get; set; }
		public object ReturnValue { get; set; }
		protected readonly object Mock;
		public List<Matcher> Matchers { get; set; }

		public Behavior(object mock) {
			Mock = mock;
			Matchers = new List<Matcher>();
		}


		internal bool IsMatch(object[] p) {
			for (int i = 0; i < p.Length; i++) {
				var matcher = Matchers[i];
				if (!matcher.IsMatch(p[i]))
					return false;
			}
			return true;
		}
	}

	public class Behavior<T> : Behavior, IBehavior<T> {

		public Behavior(object mock)
			: base(mock) {
		}

		public IBehavior<T> Returns(T returnValue) {
			ReturnValue = returnValue;
			return this;
		}

		public IBehavior ReturnsSelf() {
			ReturnValue = this.Mock;
			return this;
		}
	}
}
