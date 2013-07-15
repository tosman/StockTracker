using Machine.Specifications;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    public abstract class WithDb<TRepository> : With<TRepository> where TRepository : class
    {
        Establish context = () => Mocks.Inject(Database.GetDatabase("Test"));
    }
}