using NUnit.Framework;
using RedPencil.Domain.Products;
using RedPencil.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void IsProductRedPencil_PriceReducedByFivePercent_Yes()
        {
            // arrange
            var product = new Product 
            { 
                OriginalPrice = 15.00, 
                CurrentPrice = 14.00, 
                PriceHistories = new List<PriceHistory>(),
                CurrentPriceDateStart = DateTimeOffset.Now,
                CurrentPriceDateEnd = DateTimeOffset.Now.AddDays(5)
            };

            var updatedProduct = _productService.ProductWasUpdated(product);

            Assert.AreEqual(true, updatedProduct.PriceHistories.FirstOrDefault().IsRedPencil);
        }

        [Test]
        public void IsProductRedPencil_ProductsHavePriceHistory_PriceHistoryIsFilledOut()
        {
            // arrange
            var product = new Product
            {
                OriginalPrice = 15.00,
                CurrentPrice = 14.25,
                CurrentPriceDateStart = DateTimeOffset.Now,
                CurrentPriceDateEnd = DateTimeOffset.Now.AddDays(5),
                PriceHistories = new List<PriceHistory>
                {
                    new PriceHistory
                    {
                        DateStart = DateTimeOffset.Now.AddDays(-40),
                        DateEnd = DateTimeOffset.Now.AddDays(-35),
                        Price = 13.00
                    }
                }
            };

            // act
            var updatedProduct = _productService.ProductWasUpdated(product);

            var mostRecentPriceHistory = updatedProduct
                .PriceHistories
                .OrderByDescending(x => x.DateStart)
                .FirstOrDefault();

            Assert.AreEqual(true, mostRecentPriceHistory.IsRedPencil);
        }

        [Test]
        public void IsProductRedPencil_ProductRecentlyDiscounted_NotRedPencil()
        {
            // arrange
            var product = new Product
            {
                OriginalPrice = 15.00,
                CurrentPrice = 14.25,
                CurrentPriceDateStart = DateTimeOffset.Now,
                CurrentPriceDateEnd = DateTimeOffset.Now.AddDays(5),
                PriceHistories = new List<PriceHistory>
                {
                    new PriceHistory
                    {
                        DateStart = DateTimeOffset.Now.AddDays(-10),
                        Price = 13.00
                    }
                }
            };

            // act
            var updatedProduct = _productService.ProductWasUpdated(product);

            Assert.AreEqual(false, updatedProduct.PriceHistories.FirstOrDefault().IsRedPencil);
        }

        [Test]
        public void IsProductRedPencil_ExpirationOverThirtyDays_NotRedPencil()
        {
            // arrange
            var product = new Product
            {
                OriginalPrice = 15.00,
                CurrentPrice = 14.00,
                CurrentPriceDateStart = DateTimeOffset.Now.AddDays(-10),
                CurrentPriceDateEnd = DateTimeOffset.Now.AddDays(60),
                PriceHistories = new List<PriceHistory>()
            };

            // act
            var updatedProduct = _productService.ProductWasUpdated(product);

            Assert.AreEqual(false, updatedProduct.PriceHistories.FirstOrDefault().IsRedPencil);
        }
    }
}