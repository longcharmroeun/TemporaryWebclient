# Fix Cloudflare Update 7/1/2020

## Supported Versions
| Version | Supported          |
| ------- | ------------------ |
| 1.0.5   | :white_check_mark: |
| 1.0.4   | :x:                |
| 1.0.3   | :x:                |
| 1.0.2   | :x:                |
| 1.0.1   | :x:                |
| 1.0.0   | :x:                |

## Nuget
https://www.nuget.org/packages/TemporaryWebclient/1.0.5

## Install
### Dotnet CLI
```c#
dotnet add package TemporaryWebclient --version 1.0.5
```
### Packege Manager
```c#
Install-Package TemporaryWebclient -Version 1.0.5
```

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

## Other 
Please Use More MaxTries To Fix New Update Cloudflare.

## Issues
Some Website May not Working.
Ex. https://getwsocourse.com/

## More Document 
https://github.com/RyuzakiH/CloudflareSolverRe/blob/master/README.md
