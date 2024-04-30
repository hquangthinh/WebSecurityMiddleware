# Web Security Middleware for ASP.NET Core Web App

This is a .NET Core library that provides additional security features for web applications. It is designed to help developers secure their web applications by providing easy way to configure http response headers using configuration.

## Features

- **Add HTTP response headers**: Read response headers from the configuration and add to every HTTP response. For example adding these headers "Strict-Transport-Security", "X-Content-Type-Options".
- **Remove HTTP response headers**: Read from the configuration the response header that you want to remove from every HTTP response. For example removing these headers "X-Powered-By", "X-AspNet-Version", "X-AspNetMvc-Version".

## Installation

To install this library, run the following command in your terminal:

```powershell
dotnet add package WebSecurityMiddleware.AspNetCore.Core
```

## Usage

Here is a basic example of how to use this library:

```csharp
using WebSecurityMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.ConfigureOptions<ConfigureAppSecurityOptions>();

var app = builder.Build();

app.UseHttpsRedirection();

// Use of other middlewares

app.UseSecurityHttpResponseHeaders();

app.Run();

```

This will add the necessary services and middleware to your application.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
