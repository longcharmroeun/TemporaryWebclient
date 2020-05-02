using CloudflareSolverRe;
using CloudflareSolverRe.Types.Javascript;
using System;
using System.Net.Http;

namespace TemporaryWebclient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var target = new Uri("https://kissasian.sh/");

            var handler = new ClearanceHandler
            {
                MaxTries = 100,
                ClearanceDelay = 3000
            };

            var client = new HttpClient(handler);

            var content = client.GetStringAsync(target).Result;
            Console.WriteLine(content);

            //var tmp = JsChallenge.Parse(Resource1.String1, target);
            ////Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(tmp.Script.Calculations.GetEnumerator().Current));
            //Console.WriteLine(tmp.Solve());
        }
    }
}
