using MongoDB.Driver;

namespace StockTracker.Business.Persistence
{
    public static class Database
    {
        public const string DbName = "Stocks";

        public static MongoDatabase GetDatabase(string dbName)
        {
            const string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase(dbName);
        }

        public static MongoDatabase GetDatabase()
        {
            return GetDatabase(DbName);
        }
    }
}