using System;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(string input)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICalculatorService
    {
        int Add(string input);
    }
}
