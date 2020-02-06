using RedPencil.Entity;
using System;
using System.Linq;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {
        // this method is called whenever a product's price is updated
        // could be a service bus topic message or something

        public bool IsProductRedPencil(Product product)
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
        bool IsProductRedPencil(Product product);
    }
}
