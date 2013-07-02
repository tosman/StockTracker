using System;
using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    [Subject(typeof(StockRepository))]
    public class When_saving_a_new_stock : With<StockRepository>
    {
        Because of = () => Subject.Save(new Stock("GOOG"));
    }
}
