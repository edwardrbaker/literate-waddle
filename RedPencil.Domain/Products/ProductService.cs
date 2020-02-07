using RedPencil.Entity;
using System;
using System.Linq;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {
        // this method is called whenever a product's price is updated
        // could be from an API, or a service bus queue, or whatever

        public Product ProductWasUpdated(Product product)
        {
            // CurrentPrice is the new adjusted price
            // OriginalPrice is an original price, that (probably) never changes
                // Look at me: I am the SME now
            if (product.CurrentPrice < product.OriginalPrice)
            {
                var redPencil = IsProductRedPencil(product);

                // inactivate any previous active price histories
                product.PriceHistories.Where(ph => ph.IsActive).Select(s => { s.IsActive = false; return s; }).ToList();

                // create a new price history
                product.PriceHistories.Add(new PriceHistory
                {
                    Price = product.CurrentPrice,
                    DateStart = product.CurrentPriceDateStart,
                    DateEnd = product.CurrentPriceDateEnd,
                    IsRedPencil = redPencil,
                    IsActive = true
                });
            }

            // a database layer would persist the updated product and the price history record here, probably

            // return this back to the API, or whoever wanted the product updated
            // for the sake of testing, return the value
            return product;
        }

        private bool IsProductRedPencil(Product product)
        {
            // if the reduction is not between 5% and 30%, this cannot be a red pencil deal
            var reductionPercent = ((product.OriginalPrice - product.CurrentPrice) / product.OriginalPrice) * 100;
            if (reductionPercent < 5 || reductionPercent > 30) return false;

            // if there are any price histories that have happened in the past 30 days, this cannot be a red pencil deal
            if (product.PriceHistories.Any(date => date.DateStart > DateTimeOffset.Now.AddDays(-30)))
            {
                return false;
            }

            // if the current price markdown is set to last longer than 30 days, this cannot be a red pencil deal
            var timespan = product.CurrentPriceDateStart.Subtract(product.CurrentPriceDateEnd.Value);
            if (Math.Abs((int)timespan.TotalDays) > 30)
            {
                return false;
            }

            return true;
        }
    }

    public interface IProductService
    {
        Product ProductWasUpdated(Product product);
    }
}
