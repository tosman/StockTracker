using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    [Subject(typeof(StockRepository))]
    public class When_updating_a_stock : WithDb<StockRepository>
    {
        Establish context = () =>
        {
            _original = Subject.Save(new Stock("IBM"));
            _original.Name = "IBM Corp.";
            Subject.Save(_original);
        };

        Because of = () => _updated = Subject.Get("IBM");

        It should_update_the_name = () => _updated.Name.ShouldEqual("IBM Corp.");

        private static Stock _original;
        private static Stock _updated;
    }
}