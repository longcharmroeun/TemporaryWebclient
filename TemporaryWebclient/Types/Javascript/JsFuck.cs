using CloudflareSolverRe.Extensions;
using System.Linq;
using System.Text.RegularExpressions;

namespace CloudflareSolverRe.Types.Javascript
{
    public class JsFuck
    {
        private const string ZeroPattern = @"\[\]";
        private const string OnePattern = @"\!\+\[\]|\!\!\[\]";
        private const string DigitPattern = @"\(?(\+?(" + OnePattern + @"|" + ZeroPattern + @"))+\)?";
        private const string NumberPattern = @"\+?\(?(?<digits>\+?" + DigitPattern + @")+\)?";


        public static bool IsEncodedNumber(string number) => Regex.Match(number, NumberPattern).Success;

        public static double DecodeNumber(string encodedNumber)
        {
            Jint.Engine engine = new Jint.Engine();
            var test = engine.Execute(encodedNumber).GetCompletionValue().ToObject().ToString();
            return double.Parse(test);
        }


        public static string EncodeNumber(string number) =>
            $@"+({string.Join("+", Enumerable.Range(1, number.Length - 1)
                .Select(i => EncodeDigit(int.Parse(number[i].ToString())))
                .Prepend(EncodeDigit(int.Parse(number[0].ToString()), true)))})";

        public static string EncodeNumber(int number) =>
            $@"+({string.Join("+", Enumerable.Range(1, number.ToString().Length - 1)
                .Select(i => EncodeDigit(int.Parse(number.ToString()[i].ToString())))
                .Prepend(EncodeDigit(int.Parse(number.ToString()[0].ToString()), true)))})";

        private static string EncodeDigit(int digit, bool stringResult = false)
        {
            if (digit == 0)
                return $"(+[]{(stringResult ? "+[]" : "")})";
            else if (digit == 1)
                return $"(+!![]{(stringResult ? "+[]" : "")})";

            var encoded = Enumerable.Range(0, digit - 1).Select(d => "!![]").Prepend("!+[]");

            return $"({string.Join("+", stringResult ? encoded.Append("[]") : encoded)})";
        }


    }
}
