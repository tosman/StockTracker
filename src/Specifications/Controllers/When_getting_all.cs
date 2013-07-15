using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NSubstitute;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;
using StockTracker.Server.Controllers;

namespace StockTracker.Specifications.Controllers
{
    [Subject(typeof(StockController))]
    public class When_getting_all : With<StockController>
    {
        Establish context = () => For<IStockRepository>().GetAll().Returns(new List<Stock> {new Stock("GOOG"), new Stock("MS")});

        Because of = () => _stocks = Subject.List();

        It should_return_all_the_stocks = () => _stocks.Count().ShouldEqual(2);

        private static IEnumerable<Stock> _stocks;
    }
}
