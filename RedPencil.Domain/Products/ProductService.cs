using RedPencil.Entity;
using System;
using System.Linq;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {
        public bool IsProductRedPencil(Product product)
        {
            var reductionPercent = ((product.OriginalPrice - product.CurrentPrice) / product.OriginalPrice) * 100;
            if (reductionPercent < 5 || reductionPercent > 30) return false;

            if (product.PriceHistories.Any(date => date.DateStart > DateTimeOffset.Now.AddDays(-30)))
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
