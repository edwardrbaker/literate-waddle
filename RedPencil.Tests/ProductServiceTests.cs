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
        public void ProductWasUpdated_PriceReducedByFivePercent_IsRedPencilDeal()
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
            var priceHistory = updatedProduct.PriceHistories.FirstOrDefault(x => x.IsActive);

            Assert.AreEqual(true, priceHistory.IsRedPencil);
        }

        [Test]
        public void ProductWasUpdated_ProductsHavePriceHistory_PriceHistoryIsFilledOut()
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
                        Price = 13.00,
                        IsActive = false
                    }
                }
            };

            // act
            var updatedProduct = _productService.ProductWasUpdated(product);

            var mostRecentPriceHistory = updatedProduct
                .PriceHistories
                .FirstOrDefault(x => x.IsActive);

            Assert.AreEqual(true, mostRecentPriceHistory.IsRedPencil);
        }

        [Test]
        public void ProductWasUpdated_ProductRecentlyDiscounted_NotRedPencil()
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
            var priceHistory = updatedProduct.PriceHistories.FirstOrDefault(x => x.IsActive);

            Assert.AreEqual(false, priceHistory.IsRedPencil);
        }

        [Test]
        public void ProductWasUpdated_ExpirationOverThirtyDays_NotRedPencil()
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
            var priceHistory = updatedProduct.PriceHistories.FirstOrDefault(x => x.IsActive);

            Assert.AreEqual(false, priceHistory.IsRedPencil);
        }

        [Test]
        public void ProductWasUpdated_ActivePriceHistoryExists_InactivateAndCreateNew()
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
                        DateEnd = DateTimeOffset.Now.AddDays(5),
                        Price = 13.00,
                        IsActive = true
                    }
                }
            };

            // act
            var updatedProduct = _productService.ProductWasUpdated(product);

            var mostRecentPriceHistory = updatedProduct
                .PriceHistories
                .FirstOrDefault(x => x.IsActive);

            Assert.AreEqual(1, updatedProduct.PriceHistories.Count(p => p.IsActive));
            Assert.AreEqual(14.25, mostRecentPriceHistory.Price);
        }
    }
}