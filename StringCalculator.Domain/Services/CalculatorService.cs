using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            var delimiter = GetDelimiter(input);

            var numbers = ConvertList(input.Split(delimiter, '\n'));

            if (numbers.Any(x => x < 0))
            {
                var negs = string.Join(",", numbers.Where(x => x < 0).Select(x => x.ToString()).ToList());
                throw new ArgumentOutOfRangeException("negatives not allowed: " + negs);
            }

            return numbers.Sum();
        }

        private char GetDelimiter(string input)
        {
            if (!input.StartsWith("//")) return ',';

            var firstLine = input.Split('\n')[0];
            return firstLine.Trim('/').ToCharArray()[0];
        }

        private List<int> ConvertList(string[] numbers)
        {
            return new List<string>(
                numbers
            ).Select(s => { int i; return int.TryParse(s, out i) ? i : (int?)null;  })
            .Where(i => i.HasValue)
            .Select(i => i.Value)
            .ToList();
        }
    }

    public interface ICalculatorService
    {
        int Add(string input);
    }
}
