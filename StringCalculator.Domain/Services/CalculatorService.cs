using System;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        // per kata, we should assume the input is valid and not test
        // otherwise I'd have added some additional bounds testing on `input`
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            var numbers = input.Split(',');
            if (numbers.Length == 1)
            {
                int.TryParse(numbers[0], out int result);
                return result;
            }

            throw new ArithmeticException("wee");
        }
    }

    public interface ICalculatorService
    {
        int Add(string input);
    }
}
