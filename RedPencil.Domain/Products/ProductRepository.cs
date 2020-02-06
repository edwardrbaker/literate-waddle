using RedPencil.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedPencil.Domain.Products
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product {Id = 1, Name = "Yeti"}
            };
        }

        public Product GetProductById(int i)
        {
            return new Product { Id = i };
        }
    }

    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int i);
    }
}
