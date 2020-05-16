using System.Linq;
using System.Text.RegularExpressions;

namespace CloudflareSolverRe.Types.Javascript
{
    public class CfdnCalculation : JsCalculation, IJsCalculation
    {
        private static readonly Regex CharCodeCalculationRegex = new Regex(@"\s*?\w+?\.\w+?(?<operator>[+\-*\/])=(?<cfdn>function\(.\)\{var.*?;\s.*?;)", RegexOptions.Singleline/* | RegexOptions.Compiled*/);
        private static readonly Regex CfdnIdCodeRegex = new Regex(@"k[+\-]=(?<k>[+\-].*?);(?<CharCode>.*?;.*?;)", RegexOptions.Singleline);

        private string GetCfdnId(string kCode, string k)
        {
            return k + JsFuck.DecodeNumber(kCode).ToString();
        }

        private string GetCfdn(string cfdn, string Kcode, string k)
        {
            var Regex = "id=\"" + GetCfdnId(Kcode, k) + "\">(?<Cfnd>.*?)<";
            return new Regex(Regex, RegexOptions.Singleline).Match(cfdn).Groups["Cfnd"].Value;
        }

        public new double Result { get => Solve(); }

        public string Cfdn { get; set; }

        public CfdnCalculation(string calculation, string cfdn, string k)
        {
            var KandCharCode = CfdnIdCodeRegex.Match(calculation);
            string KCode = KandCharCode.Groups["k"].Value;

            Type = CalculationType.Cfdn;
            Value = calculation;
            First = Cfdn = GetCfdn(cfdn, KCode, k);

            Operator = CharCodeCalculationRegex.Match(KandCharCode.Groups["CharCode"].Value).Groups["operator"].Value;
        }

        public new double Solve() => new NormalCalculation($"test.temp{Operator}={Cfdn};").Solve(); //JsFuck.DecodeNumber(First);
    }
}