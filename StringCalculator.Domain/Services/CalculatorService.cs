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
            
            var inputNormalized = NormalizeString(input);
            var numbers = ConvertList(inputNormalized);

            if (numbers.Any(x => x < 0))
            {
                var negs = string.Join(",", numbers.Where(x => x < 0).Select(x => x.ToString()).ToList());
                throw new ArgumentOutOfRangeException("negatives not allowed: " + negs);
            }

            return numbers.Where(x => x <= _maxNumber).Sum();
        }

        /* 
         * Normalize the input string so that all delimiters will be turned into simple commas
         */
        private string[] NormalizeString(string input)
        {
            // check for custom delim string
            if (input.StartsWith("//["))
            {
                var limitLine = input.Split('\n')[0];
                var longDelimiter = limitLine.Replace("//[", "").Replace("]", "");
                input = input.Replace(limitLine, "").Replace(longDelimiter, ",");
            }
            else if (input.StartsWith("//"))
            {
                var firstLine = input.Split('\n')[0];
                var delimiter = firstLine.Trim('/');
                input = input.Replace(delimiter, ",");
            }

            // newlines should be delimiters
            input = input.Replace("\n", ",");

            return input.Split(',');
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
