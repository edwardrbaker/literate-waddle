using Moq;
using NUnit.Framework;
using RedPencil.Domain.Products;
using RedPencil.Entity;
using System.Collections.Generic;

namespace RedPencil.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductService _productService;
        private Mock<IProductRepository> _productRepo;

        [SetUp]
        public void Setup()
        {
            _productRepo = new Mock<IProductRepository>();

            _productService = new ProductService(_productRepo.Object);
        }

        [Test]
        public void GetProducts_OneProductExists_ReturnProducts()
        {
            var product = new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Yeti"
                }
            };

            // arrange
            _productRepo.Setup(mock => mock.GetAllProducts()).Returns(product);

            var products = _productService.GetProducts();

            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void GetProducts_WhenMultipleProductsExist_ReturnAll()
        {
            var product = new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Yeti"
                },
                new Product
                {
                    Id = 2,
                    Name = "My Pillow"
                }
            };

            // arrange
            _productRepo.Setup(mock => mock.GetAllProducts()).Returns(product);

            var products = _productService.GetProducts();

            Assert.AreEqual(2, products.Count);
        }
    }
}