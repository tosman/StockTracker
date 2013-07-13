using System.Configuration;
using MongoDB.Driver;
using StockTracker.Business.Models;

namespace StockTracker.Business.Persistence
{
    public interface IDatabase
    {
        void Start(string database);
        MongoCollection<Stock> GetStocksCollection();
    }

    public class Database : IDatabase
    {
        private MongoDatabase _db;

        public void Start(string database)
        {
            //var client = new MongoClient(ConfigurationManager.ConnectionStrings["main"].ConnectionString);
            var client = new MongoClient("mongodb://localhost");
            var server = client.GetServer();
            _db = server.GetDatabase(database);
        }

        public MongoCollection<Stock> GetStocksCollection()
        {
            return _db.GetCollection<Stock>("Stocks");
        }
    }
}