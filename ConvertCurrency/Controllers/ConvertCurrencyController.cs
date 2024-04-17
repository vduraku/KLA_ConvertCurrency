using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ConvertCurrency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConvertCurrencyController : ControllerBase
    {

        static string[] Ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static string[] Teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static string[] Tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        static string[] Thousands = { "", "thousand", "million", "billion" };


        [HttpGet, Route("checknumber")]
        public IActionResult ConvertToWords(string amount)
        {
            var amounToDecimal = Convert.ToDecimal(amount);
            if (amounToDecimal < 0 || amounToDecimal > 999999999.99m)
            {
                throw new HttpRequestException("Invalid amount");//check for invalid numbers to convert
            }

            var dollars = (int)Math.Floor(amounToDecimal);
            var cents = (int)((amounToDecimal - dollars) * 100);

            var result = $"{ConvertToWords(dollars)} dollars";

            if (cents > 0)
            {
                result += $" and {ConvertToWords(cents)} cents";
            }

            return Ok(new { result });
           // return Ok(result);
        }

        static string ConvertToWords(int number)
        {
            int num = number;
            if (num == 0)
            {
                return "zero";
            }

            string result = "";
            if (num / 100 > 0)
            {
                result += Ones[num / 100] + " hundred ";
                num %= 100;
            }

            if (num >= 20)
            {
                result += Tens[num / 10] + " ";
                num %= 10;
            }
            else if (num >= 10)
            {
                result += Teens[num - 10] + " ";
                num = 0;
            }

            if (num > 0)
            {
                result += Ones[num] + " ";
            }

            return result.Trim();
        }
    }

  
}