using System.Collections.Generic;
using System.Web.Http;
using StockTracker.Business.Models;
using StockTracker.Business.Persistence;

namespace StockTracker.Server.Controllers
{
    public class StockController : ApiController
    {
        private readonly IStockRepository _repository;

        public StockController(IStockRepository repository)
        {
            _repository = repository;
        }

        // GET api/stock
        [HttpGet]
        public IEnumerable<Stock> List()
        {
            return new List<Stock> {new Stock("GOOG")};
        }

        // GET api/stock/5
        [HttpGet]
        public Stock Get(int id)
        {
            return new Stock();
        }

        // POST api/stock
        [HttpPost]
        public void Create([FromBody]Stock value)
        {
        }

        // PUT api/stock/5
        [HttpPut]
        public void Update(int id, [FromBody]Stock value)
        {
        }

        // DELETE api/stock/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
