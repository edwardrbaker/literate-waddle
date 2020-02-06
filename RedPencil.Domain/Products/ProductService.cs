using RedPencil.Entity;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {

        public bool IsProductRedPencil(Product product)
        {
            return false;
        }
    }

    public interface IProductService
    {
        bool IsProductRedPencil(Product product);
    }
}
