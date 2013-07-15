using System;
using MongoDB.Bson;

namespace StockTracker.Business.Models
{
    public class Stock
    {
        public ObjectId Id { get; set; }
        public string Symbol { get; private set; }
        public string Name { get; set; }
        public string Index { get; private set; }
        public string Category { get; private set; }
        public string Risk { get; private set; }
        public DateTime? EarningsDate { get; private set; }

        public Stock()
        {

        }

        public Stock(string symbol)
        {
            Symbol = symbol;
        }

        public Stock(string symbol, string name, string index, string category, string risk, DateTime? earningsDate)
        {
            Symbol = symbol;
            Name = name;
            Index = index;
            Category = category;
            Risk = risk;
            EarningsDate = earningsDate;
        }
    }
}