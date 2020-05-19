# Important
Thank for owner library: https://github.com/RyuzakiH/CloudflareSolverRe

## Fix Cloudflare Update 5/16/2020

## Nuget
https://www.nuget.org/packages/TemporaryWebclient/1.0.4

## Simple
```c#
var target = new Uri("https://kissanime.ru/");

            var handler = new ClearanceHandler
            {
                MaxTries = 3,
                ClearanceDelay = 3000
            };

            var client = new HttpClient(handler);

            var content = client.GetStringAsync(target).Result;
            Console.WriteLine(content);
```


## This library is the same as https://github.com/RyuzakiH/CloudflareSolverRe it just use third party dependencies.
## Document 
https://github.com/RyuzakiH/CloudflareSolverRe/blob/master/README.md
