using NUnit.Framework;
using RedPencil.Domain.Products;
using RedPencil.Entity;

namespace RedPencil.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService();
        }

        [Test]
        public void IsProductRedPencil_DefaultIsFalse_ReturnsAnswer()
        {
            // arrange
            var product = new Product { };

            var isProductRedPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(false, isProductRedPencil);
        }

        [Test]
        public void IsProductRedPencil_PriceReducedByFivePercent_Yes()
        {
            // arrange
            var product = new Product { OriginalPrice = 15.00, CurrentPrice = 14.25 };

            var redPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(true, redPencil);
        }
    }
}