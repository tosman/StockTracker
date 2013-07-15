using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using StockTracker.Business.Models;

namespace StockTracker.Business.Persistence
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetAll();
        Stock Get(string symbol);
        Stock Save(Stock stock);
    }

    public class StockRepository : IStockRepository
    {
        private readonly MongoDatabase _db;

        public StockRepository(MongoDatabase db)
        {
            _db = db;
        }

        public IEnumerable<Stock> GetAll()
        {
            return _db.GetCollection<Stock>("Stocks").FindAll();
        }

        public Stock Get(string symbol)
        {
            var collection = _db.GetCollection<Stock>("Stocks");

            return collection.FindOne(Query<Stock>.EQ(x => x.Symbol, symbol));
        }

        public Stock Save(Stock stock)
        {
            return stock.Id == ObjectId.Empty ? Insert(stock) : Update(stock);
        }

        private Stock Insert(Stock stock)
        {
            var collection = _db.GetCollection<Stock>("Stocks");
            collection.Insert(stock);
            return stock;
        }

        private Stock Update(Stock stock)
        {
            var collection = _db.GetCollection<Stock>("Stocks");
            collection.Save(stock);
            return stock;
        }
    }
}
