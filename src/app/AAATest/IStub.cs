using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAATest.Framework;
using Moq.Language.Flow;
using System.Linq.Expressions;

namespace AAATest
{
	public interface IStub<T> where T : class {
		void Stub(Mock<T> stub, DependencyManager deps);
	}

	/// <summary>
	/// Represents a stubbed method, different principal to mock
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Stub<T> where T : class {

		//get rid of stub
		private readonly Moq.IMock<T> Mock;
		public int Count = 0;
		public T Object; //the mock object

		public Stub(Moq.IMock<T> mock) {
			Mock = mock;
		}

	}

	public class Mock<T> : Stub<T> where T : class {
		public Mock(Moq.IMock<T> mock) : base(mock) {
		}
	}

	public class Mock<T, TReturn> : Stub<T> where T : class {
		public Mock(Moq.IMock<T> mock) : base(mock) {
		}

		public void Returns(TReturn returnValue) { }
	}

	public class StubProvider<T> where T : class {

		public T Object { get; private set; }
		public Moq.Mock<T> Moq { get; private set; }

		protected virtual void InitializeData() {
		}

		protected virtual void Initialize() {
		}

		//public ISetup<T, Z> Arrange<Z>(Expression<Func<T, Z>> expr) {
			//throw new NotImplementedException();
		//}


		/// <summary>
		/// needs return function otherwise there is no point wrapping moq
		/// </summary>
		/// <typeparam name="Z"></typeparam>
		/// <param name="expr"></param>
		/// <param name="returns"></param>
		/// <returns></returns>
		public Mock<T, TReturn> Arrange<TReturn>(Expression<Func<T, TReturn>> expr) {
			var moq = new Moq.Mock<T>();
			var stub = new Mock<T, TReturn>(moq);
			var setup = moq.Setup(expr).Callback(() => { stub.Count++; });
			return stub;
		}

		public Stub<T> Arrange(Expression<Action<T>> expr) {
			var moq = new Moq.Mock<T>();
			var stub = new Stub<T>(moq);
			var setup = moq.Setup(expr).Callback(() => { stub.Count++; });
			return stub;
		}
	}
}
