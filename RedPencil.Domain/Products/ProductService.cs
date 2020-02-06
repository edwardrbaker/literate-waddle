using RedPencil.Entity;
using System;
using System.Collections.Generic;

namespace RedPencil.Domain.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public List<Product> GetProducts()
        {
            return _productRepo.GetAllProducts();
        }
    }

    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
