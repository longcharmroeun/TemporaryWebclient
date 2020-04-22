using CloudflareSolverRe.Types.Javascript;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudflareSolverRe
{
    public class TemporarilyWebclient
    {
        public string UserAgent { get; set; }
        public Uri Uri { get; set; }
        public CookieCollection Cookies { get; set; }

        private string CookieContainerToString()
        {
            string _tmp = string.Empty;
            for (int i = 0; i < Cookies.Count; i++)
            {
                _tmp += Cookies[i].Name + "=" + Cookies[i].Value + ";";
            }
            _tmp.Remove(_tmp.Length - 1);
            return _tmp;
        }

        public string GetStringData()
        {
            WebRequest request = HttpWebRequest.Create(Uri.AbsoluteUri);
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
            request.Headers.Add(HttpRequestHeader.Referer, Uri.AbsoluteUri);
            using var res = (HttpWebResponse)request.GetResponse();

            string html = string.Empty;
            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                html = reader.ReadToEnd();
            }
            try
            {
                foreach (var item in res.Headers.GetValues("Set-Cookie"))
                {
                    var _tmp = item.Split(';')[0].Split('=');
                    Cookies.Add(new Cookie(_tmp[0], _tmp[1]));
                }
            }
            catch (NullReferenceException)
            {

            }
            return html;
        }

        public async Task<string> PostDataAsync(string url, FormUrlEncodedContent formUrlEncodedContent, string referer = "")
        {
            string tmp = await formUrlEncodedContent.ReadAsStringAsync();
            byte[] data = Encoding.ASCII.GetBytes(tmp);

            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "POST";
            if (!string.IsNullOrEmpty(referer))
            {
                request.Headers.Add(HttpRequestHeader.Referer, referer);
            }
            request.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);
            request.Headers.Add(HttpRequestHeader.Referer, Uri.AbsoluteUri);
            request.Headers.Add("Origin", Uri.AbsoluteUri);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var res = request.GetResponse();

            string html = string.Empty;

            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                html = reader.ReadToEnd();
            }

            try
            {
                foreach (var item in res.Headers.GetValues("Set-Cookie"))
                {
                    var _tmp = item.Split(';')[0].Split('=');
                    Cookies.Add(new Cookie(_tmp[0], _tmp[1]));
                }
            }
            catch (NullReferenceException)
            {

            }
            return html;
        }

        public async Task<WebClient> GetWebClientAsync()
        {

            try
            {
                var content = GetStringData();
            }
            catch (WebException ex)
            {
                string html = string.Empty;
                using (var steam = ex.Response.GetResponseStream())
                {
                    StreamReader streamReader = new StreamReader(steam, Encoding.UTF8);
                    html = streamReader.ReadToEnd();
                }
                //Console.WriteLine(html, Color.Green);

                JsChallenge jsChallenge = JsChallenge.Parse(html, Uri);
                //Console.WriteLine(jsChallenge.Solve(), Color.Gray);

                JsChallengeSolution jsChallengeS = new JsChallengeSolution(Uri, jsChallenge.Form, jsChallenge.Solve());

                Thread.Sleep(3000);
                _ = await PostDataAsync(jsChallengeS.ClearanceUrl, new FormUrlEncodedContent(jsChallengeS.ClearanceBody));
            }

            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Cookie, this.CookieContainerToString());
            webClient.Headers.Add(HttpRequestHeader.UserAgent, this.UserAgent);
            return webClient;
        }

        public TemporarilyWebclient(string userAgent, Uri uri)
        {
            this.UserAgent = userAgent;
            this.Uri = uri;
            this.Cookies = new CookieCollection();
        }
    } 
}
