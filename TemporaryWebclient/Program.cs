using CloudflareSolverRe;
using System;
using System.Net.Http;

namespace TemporaryWebclient
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var target = new Uri("https://slushpool.com/home/");

            var handler = new ClearanceHandler
            {
                MaxTries = 3,
                ClearanceDelay = 3000
            };

            var client = new HttpClient(handler);

            var content = client.GetStringAsync(target).Result;
            Console.WriteLine(content);
        }
    }
}
