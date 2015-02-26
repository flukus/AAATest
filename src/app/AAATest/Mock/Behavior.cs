using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;

namespace AAATest.Mock {
	public class Behavior : IBehavior {
		public int CallCount { get; set; }
		public object ReturnValue { get; set; }
		//protected readonly object Mock;
		public List<Matcher> Matchers { get; set; }
		protected object Mock;

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

	public class Behavior<TReturn> : Behavior, IBehavior<TReturn> {

		private readonly Mockery Mockery;

		public Behavior(object mock, Mockery mockery)
			: base(mock) {
			Mockery = mockery;
		}

		public IBehavior<TReturn> Returns(TReturn returnValue) {
			ReturnValue = returnValue;
			return this;
		}

		public IBehavior<TReturn> Returns<TMock>() where TMock : class, TReturn {
			ReturnValue =  Mockery.GetMock<TMock>();
			return this ;
		}

		public IBehavior ReturnsSelf() {
			ReturnValue = this.Mock;
			return this;
		}
	}
}
