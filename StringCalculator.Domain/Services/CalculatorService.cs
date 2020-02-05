using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Domain.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly int _maxNumber = 1000;

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            var delimiters = GetDelimiters(input);

            var numbers = ConvertList(input.Split(delimiters));

            if (numbers.Any(x => x < 0))
            {
                var negs = string.Join(",", numbers.Where(x => x < 0).Select(x => x.ToString()).ToList());
                throw new ArgumentOutOfRangeException("negatives not allowed: " + negs);
            }

            return numbers.Where(x => x <= _maxNumber).Sum();
        }

        private char[] GetDelimiters(string input)
        {
            List<char> delimResult = new List<char> { '\n' }; // this will always be a delimiter

            // if the input string does not have the // to denote a custom delimiter, add commas and return
            if (!input.StartsWith("//"))
            {
                delimResult.Add(',');
                return delimResult.ToArray();
            }

            var firstLine = input.Split('\n')[0];
            delimResult.Add(firstLine.Trim('/').ToCharArray()[0]);

            return delimResult.ToArray();
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
