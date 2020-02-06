using RedPencil.Entity;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {

        public bool IsProductRedPencil(Product product)
        {
            var reductionPercent = ((product.OriginalPrice - product.CurrentPrice) / product.OriginalPrice) * 100;
            
            if (reductionPercent >= 5 && reductionPercent <= 30) return true;

            return false;
        }
    }

    public interface IProductService
    {
        bool IsProductRedPencil(Product product);
    }
}
