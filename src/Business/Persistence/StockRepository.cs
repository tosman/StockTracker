using MongoDB.Bson;
using MongoDB.Driver.Builders;
using StockTracker.Business.Models;

namespace StockTracker.Business.Persistence
{
    public class StockRepository
    {
        private readonly IDatabase _db;

        public StockRepository(IDatabase db)
        {
            _db = db;
        }

        public Stock Save(Stock stock)
        {
            if (stock.Id == ObjectId.Empty)
                return Insert(stock);

            return Update(stock);
        }

        public Stock Get(string symbol)
        {
            var collection = _db.GetStocksCollection();

            return collection.FindOne(Query<Stock>.EQ(x => x.Symbol, symbol));
        }

        private Stock Insert(Stock stock)
        {
            var collection = _db.GetStocksCollection();
            collection.Insert(stock);
            return stock;
        }

        private Stock Update(Stock stock)
        {
            var collection = _db.GetStocksCollection();
            collection.Save(stock);
            return stock;
        }
    }
}
