using NUnit.Framework;
using StringCalculator.Domain.Services;
using System;

namespace StringCalculatorTests
{
    [TestFixture]
    public class CalculatorServiceTests
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

        [Test]
        public void Add_InputTwoNumbers_ReturnsSum()
        {
            // Arrange
            var input = "1,2";

            // Act
            var result = _calculator.Add(input);

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void Add_InputMultipleNumbers_ReturnsSum()
        {
            // Arrange
            var input = "1,2,3,4,5,6,7,8,9,10";

            // Act
            var result = _calculator.Add(input);

            // Assert
            Assert.AreEqual(55, result);
        }

        [Test]
        public void Add_UseNewlineDelimeter_ReturnsSum()
        {
            var input = "1\n2,3";

            var result = _calculator.Add(input);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_UseCustomDelimiter_ReturnsSum()
        {
            var input = "//;\n1;2;";

            var result = _calculator.Add(input);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Add_UsingNegativeNumbers_ThrowsError()
        {
            var input = "1,2,3,-4";

            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Add(input), "negatives not allowed: -4");
        }

        [Test]
        public void Add_MultipleNegatives_ThrowsErrorWithAllNegatives()
        {
            var input = "1,-2,3,-4";

            Assert.Throws<ArgumentOutOfRangeException>(() => _calculator.Add(input), "negatives not allowed: -2,-4");
        }

        [Test]
        public void Add_LargeNumbersIgnored_ReturnSum()
        {
            var input = "1,2,1001";

            var result = _calculator.Add(input);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Add_SupportLargeDelimiters_ReturnSum()
        {
            var input = "//[,,,]\n1,,,2,,,3";

            var result = _calculator.Add(input);

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_SupportWeirdDelimiters_ReturnSum()
        {
            var input = "//[.#.2,&]\n1,2.#.2,&3,4,1004";

            var result = _calculator.Add(input);

            Assert.AreEqual(10, result);
        }

        [Test]
        public void Add_MultipleCustomDelimiters_ReturnsSum()
        {
            var input = "//[*][%]\n1*2%3\n4";

            var result = _calculator.Add(input);

            Assert.AreEqual(10, result);
        }

        [Test]
        public void Add_MultipleDelimitersLong_ReturnSum()
        {
            var input = "//[**][....]\n1**2....3\n4";

            var result = _calculator.Add(input);

            Assert.AreEqual(10, result);
        }
    }
}