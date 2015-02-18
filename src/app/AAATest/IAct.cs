using System;
namespace AAATest
{
    public interface IAct<T>
    {
        void Act(Action<T> action);
        void Act<A>(Func<T, A> action);
    }
}
