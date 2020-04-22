# Important
This 

## Example
```
CloudflareSolverRe.TemporarilyWebclient temporaryWebclient = new CloudflareSolverRe.TemporarilyWebclient("Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1",
                new Uri("https://kissasian.sh/"));
            var webClient = await temporaryWebclient.GetWebClientAsync();

            Console.WriteLine(webClient.DownloadString("https://kissasian.sh/"));
```
