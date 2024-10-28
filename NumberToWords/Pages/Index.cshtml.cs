using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NumberToWords.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string NumberInput { get; set; }

        public string? ConversionResult { get; private set; }

        public void OnGet()
        {
            ConversionResult = null;
        }

        public void OnPost()
        {
            if (decimal.TryParse(NumberInput, out decimal number))
            {
                ConversionResult = ConvertNumberToWords(number);
            }
            else
            {
                ConversionResult = "Please enter a valid number.";
            }
        }

        private readonly string[] zeroToNineteen = ["ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"];
        private readonly string[] twentyToNinety = ["0", "10", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"];
        private readonly decimal[] scales = [
            1_000m, // 10^3 
            1_000_000m, // 10^6
            1_000_000_000m, // 10^9
            1_000_000_000_000m, // 10^12
            1_000_000_000_000_000m, // 10^15
            1_000_000_000_000_000_000m, // 10^18
            1_000_000_000_000_000_000_000m, // 10^21
            1_000_000_000_000_000_000_000_000m, // 10^24
            1_000_000_000_000_000_000_000_000_000m, // 10^27
        ];
        private readonly string[] scaleWords = ["THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION", "QUINTILLION", "SEXTILLION", "SEPTILLION", "OCTILLION"];
        private readonly string outOfRangeMessage = "Out of range. Please enter a dollar amount between $0.00 and $79,228,162,514,264,337,593,543,950,335.00.";

        private string ConvertNumberToWords(decimal number)
        {
            if (number < 0 || number > System.Decimal.MaxValue) // 79,228,162,514,264,337,593,543,950,335
            {
                return outOfRangeMessage;
            }

            var words = "";

            // Convert the integer and fractional parts to words separately
            var integerPart = Math.Truncate(number);
            var fractionalPart = (int)((number - integerPart) * 100);

            if (integerPart > 0)
            {
                // Range [1, 999_999_999]
                words += ConvertDecimalToWords(integerPart);
                words += integerPart == 1 ? " DOLLAR" : " DOLLARS";
                if (fractionalPart > 0)
                {
                    words += " AND ";
                }
            }
            if (fractionalPart > 0)
            {
                // Range [1, 99]
                words += ConvertDecimalToWords(fractionalPart);
                words += fractionalPart == 1 ? " CENT" : " CENTS";
            }

            if (words.Length == 0)
            {
                return "ZERO DOLLARS";
            }

            return words;
        }

        private string ConvertLargeNumbers(decimal number, int index)
        {
            decimal scale = scales[index];
            // Range [scale, scale * 999]

            decimal significantDigits = Math.Truncate(number / scale); // Range [1, 999] 
            decimal restOfNumber = decimal.Remainder(number, scale); // Range [0, scale - 1]
            string result = ConvertDecimalToWords(significantDigits) + " " + scaleWords[index];
            if (restOfNumber > 0)
            {
                result += ", ";
                if (restOfNumber < 100)
                {
                    result += "AND ";
                }
                result += ConvertDecimalToWords(restOfNumber);
            }
            return result;
        }

        private string ConvertDecimalToWords(decimal number)
        {
            // This method converts the integer part to words
            string result;
            if (number == 0)
            {
                return "";
            }

            if (number < 20)
            {
                // Range [1, 19]
                result = zeroToNineteen[(int)number];
                return result;
            }

            if (number < 100)
            {
                // Range [20, 99]
                decimal onesDigit = decimal.Remainder(number, 10); // Range [0, 9]
                int tensDigit = (int)Math.Truncate(number / 10); // Range [2, 9]
                result = twentyToNinety[tensDigit] + (onesDigit != 0 ? "-" : "") + ConvertDecimalToWords(onesDigit);
                return result;
            }

            if (number < 1_000)
            {
                // Range [100, 999]
                int hundredsDigit = (int)Math.Truncate(number / 100); // Range [1, 9]
                decimal restOfNumber = decimal.Remainder(number, 100); // Range [0, 99]
                result = $"{zeroToNineteen[hundredsDigit]} HUNDRED";
                if (restOfNumber > 0)
                {
                    result += " AND ";
                    result += ConvertDecimalToWords(restOfNumber);
                }
                return result;
                //return ConvertLargeIntegers(number, 0);
            }

            foreach (var scale in scales)
            {
                if (number < scale)
                {
                    return ConvertLargeNumbers(number, Array.IndexOf(scales, scale) - 1);
                }
            }
            return ConvertLargeNumbers(number, scales.Length - 1);
        }
    }
}


