using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    public abstract class WithDb<TRepository> : With<TRepository> where TRepository : class
    {
        Establish context = () =>
        {
            var db = Database.GetDatabase("Test");
            Mocks.Inject(db);
            db.GetCollection<Stock>(StockRepository.StockCollection).Drop();
        };
    }
}