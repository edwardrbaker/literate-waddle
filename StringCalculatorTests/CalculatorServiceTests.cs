using NUnit.Framework;
using StringCalculator.Domain.Services;

namespace StringCalculatorTests
{
    [TestFixture]
    public class Tests
    {
        private ICalculatorService _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new CalculatorService();
        }

        [Test]
        public void Add_InputIsEmpty_ReturnZero()
        {
            // Arrange
            var input = "";

            // Act
            var result = _calculator.Add(input);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_InputOneNumber_ReturnSameNumber()
        {
            // Arrange
            var input = "1";

            // Act
            var result = _calculator.Add(input);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}