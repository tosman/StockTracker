using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockTracker.Business.Models
{
    public class Stock
    {
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public string Index { get; private set; }
        public string Category { get; private set; }
        public string Risk { get; private set; }
        public DateTime? EarningsDate { get; private set; }

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