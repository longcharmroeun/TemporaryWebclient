using System.Linq;
using System.Text.RegularExpressions;

namespace CloudflareSolverRe.Types.Javascript
{
    public class CfdnCalculation : JsCalculation, IJsCalculation
    {
        private static readonly Regex CharCodeCalculationRegex = new Regex(@"\s*?\w+?\.\w+?(?<operator>[+\-*\/])=(?<cfdn>function\(.\)\{var.*?;\s.*?;)", RegexOptions.Singleline/* | RegexOptions.Compiled*/);
        private static readonly Regex CfdnIdCodeRegex = new Regex(@"k[+\-]=(?<k>[+\-].*?);(?<CharCode>.*?;.*?;)", RegexOptions.Singleline);

        private string GetCfdnId(string cfdn, string k)
        {
            var cfdnId = new Regex("<div id=\"(?<cfdnId>.*?)\">", RegexOptions.Singleline).Matches(cfdn).FirstOrDefault().Groups["cfdnId"].Value;
            var kCode = cfdnId.Remove(cfdnId.Length - 1);
            return kCode + JsFuck.DecodeNumber(k).ToString();
        }

        private string GetCfdn(string cfdn, string Kcode)
        {
            var Regex = "id=\"" + GetCfdnId(cfdn, Kcode) + "\">(?<Cfnd>.*?)<";
            return new Regex(Regex, RegexOptions.Singleline).Match(cfdn).Groups["Cfnd"].Value;
        }

        public new double Result { get => Solve(); }

        public string Cfdn { get; set; }

        public CfdnCalculation(string calculation, string cfdn)
        {
            var KandCharCode = CfdnIdCodeRegex.Match(calculation);
            string KCode = KandCharCode.Groups["k"].Value;

            Type = CalculationType.Cfdn;
            Value = calculation;
            First = Cfdn = GetCfdn(cfdn, KCode);

            Operator = CharCodeCalculationRegex.Match(KandCharCode.Groups["CharCode"].Value).Groups["operator"].Value;
        }

        public new double Solve() => new NormalCalculation($"test.temp{Operator}={Cfdn};").Solve(); //JsFuck.DecodeNumber(First);
    }
}