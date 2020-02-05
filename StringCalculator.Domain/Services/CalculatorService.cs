using System;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            var delimiter = GetDelimiter(input);

            var numbers = input.Split(delimiter, '\n');

            int result = 0;
            foreach(var number in numbers)
            {
                int.TryParse(number, out var parsedValue);
                result += parsedValue;
            }

            return result;
        }

        private char GetDelimiter(string input)
        {
            if (!input.StartsWith("//")) return ',';

            var firstLine = input.Split('\n')[0];
            return firstLine.Trim('/').ToCharArray()[0];
        }
    }

    public interface ICalculatorService
    {
        int Add(string input);
    }
}
