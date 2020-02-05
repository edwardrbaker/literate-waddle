using System;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            throw new ArithmeticException("wee");
        }
    }

    public interface ICalculatorService
    {
        int Add(string input);
    }
}
