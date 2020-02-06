using NUnit.Framework;
using RedPencil.Domain.Products;
using RedPencil.Entity;
using System;
using System.Collections.Generic;

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
        public void IsProductRedPencil_DefaultIsTrue_ReturnsAnswer()
        {
            // arrange
            var product = new Product { PriceHistories = new List<PriceHistory>() };

            var isProductRedPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(true, isProductRedPencil);
        }

        [Test]
        public void IsProductRedPencil_PriceReducedByFivePercent_Yes()
        {
            // arrange
            var product = new Product { OriginalPrice = 15.00, CurrentPrice = 14.00, PriceHistories = new List<PriceHistory>() };

            var redPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(true, redPencil);
        }

        [Test]
        public void IsProductRedPencil_ProductsHavePriceHistory_PriceHistoryIsFilledOut()
        {
            // arrange
            var product = new Product
            {
                OriginalPrice = 15.00,
                CurrentPrice = 14.25,
                PriceHistories = new List<PriceHistory>
                {
                    new PriceHistory
                    {
                        LastDateChange = DateTimeOffset.Now.AddDays(-30),
                        Price = 13.00
                    }
                }
            };

            // act
            var redPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(true, redPencil);
        }

        [Test]
        public void IsProductRedPencil_ProductRecentlyDiscounted_NotRedPencil()
        {
            // arrange
            var product = new Product
            {
                OriginalPrice = 15.00,
                CurrentPrice = 14.25,
                PriceHistories = new List<PriceHistory>
                {
                    new PriceHistory
                    {
                        LastDateChange = DateTimeOffset.Now.AddDays(-10),
                        Price = 13.00
                    }
                }
            };

            // act
            var redPencil = _productService.IsProductRedPencil(product);

            Assert.AreEqual(false, redPencil);
        }
    }
}