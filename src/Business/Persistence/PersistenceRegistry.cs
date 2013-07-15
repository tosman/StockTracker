using StructureMap.Configuration.DSL;

namespace StockTracker.Business.Persistence
{
    public class PersistenceRegistry : Registry
    {
        public PersistenceRegistry()
        {
            For<IStockRepository>().Use(new StockRepository(Database.GetDatabase()));
        }
    }
}