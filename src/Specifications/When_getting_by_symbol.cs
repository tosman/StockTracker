using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    [Subject(typeof(StockRepository))]
    public class When_getting_by_symbol : WithDb<StockRepository>
    {
        Establish context = () => Subject.Save(new Stock("GRPN"));

        Because of = () => _stock = Subject.Get("GRPN");

        It should_retrieve_the_stock = () => _stock.Symbol.ShouldEqual("GRPN");

        private static Stock _stock;
    }
}