using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    [Subject(typeof(StockRepository))]
    public class When_getting_all : WithDb<StockRepository>
    {
        Establish context = () =>
        {
            Subject.Save(new Stock("GOOG"));
            Subject.Save(new Stock("MS"));
        };

        Because of = () => _stocks = Subject.GetAll();

        It should_get_all_stocks_in_the_db = () => _stocks.Count().ShouldEqual(2);

        private static IEnumerable<Stock> _stocks;
    }
}