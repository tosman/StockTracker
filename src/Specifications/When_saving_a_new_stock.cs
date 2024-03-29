﻿using Machine.Specifications;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Specifications
{
    [Subject(typeof(StockRepository))]
    public class When_saving_a_new_stock : With<StockRepository>
    {
        Because of = () => _stock = Subject.Save(new Stock("GOOG"));

        It should_save_the_stock = () => _stock.Id.ShouldNotBeNull();

        private static Stock _stock;
    }
}
