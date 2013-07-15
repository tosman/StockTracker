using System;
using Machine.Specifications;
using StructureMap.AutoMocking;

namespace StockTracker.Specifications
{
    public abstract class With<T> 
        where T : class
    {
        Establish context = () =>
        {
            Mocks = (AutoMocker<T>)NSubstituteAutoMockerBuilder.Build<T>();
            Context = new OrderedContext();
        };

        protected static void Record<TDependency>()
            where TDependency : class
        {
            Context.Record(For<TDependency>());
        }

        protected static void CallsShouldBeLike(Action expected)
        {
            Context.CallsShouldBeLike(expected);
        }

        protected static TDependency For<TDependency>()
            where TDependency : class
        {
            return (typeof(TDependency).IsInterface) ? Mocks.Get<TDependency>() : Mocks.Container.GetInstance<TDependency>();
        }

        protected static AutoMocker<T> Mocks { get; private set; }
        protected static T Subject { get { return Mocks.ClassUnderTest; } }
        protected static OrderedContext Context { get; private set; }
    }
}