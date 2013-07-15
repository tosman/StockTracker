using Machine.Specifications;
using NSubstitute;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;
using StockTracker.Server.Controllers;

namespace StockTracker.Specifications.Controllers
{
    [Subject(typeof(StockController))]
    public class When_updating_a_stock : With<StockController>
    {
        Because of = () => Subject.Update(2, _stock);

        It should_save_the_updated_stock = () => For<IStockRepository>().Received().Save(_stock);

        private static Stock _stock = new Stock("updated");
    }
}