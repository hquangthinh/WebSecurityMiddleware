using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebSecurityMiddleware.UnitTests;

public class SecurityHttpResponseHeaderMiddlewareTests
{
    [Theory]
    [InlineData("http://localhost/", "appsettings.json")]
    [InlineData("http://localhost/BasePath/", "appsettings.json")]
    public async Task SecurityHttpResponseHeaderMiddleware_Test_Add(string basePath, string configFile)
    {
        using var host = await CreateAndStartHostAsync(basePath, configFile);

        var response = await host.GetTestClient().GetAsync(basePath);
        response.Headers.TryGetValues("X-Content-Type-Options", out var headerValue1);
        response.Headers.TryGetValues("Strict-Transport-Security", out var headerValue2);
        response.Headers.TryGetValues("X-Powered-By", out var headerValue3);
        Assert.NotNull(headerValue1);
        Assert.Equal("nosniff", headerValue1.FirstOrDefault());
        
        Assert.NotNull(headerValue2);
        Assert.Equal("max-age=1800; includeSubDomains; preload", headerValue2.FirstOrDefault());
        
        Assert.NotNull(headerValue3);
        Assert.Equal("Dotnet", headerValue3.FirstOrDefault());
    }
    
    [Theory]
    [InlineData("http://localhost/", "appsettings1.json")]
    [InlineData("http://localhost/BasePath/", "appsettings1.json")]
    public async Task SecurityHttpResponseHeaderMiddleware_Test_Remove(string basePath, string configFile)
    {
        using var host = await CreateAndStartHostAsync(basePath, configFile);

        var response = await host.GetTestClient().GetAsync(basePath);
        response.Headers.TryGetValues("X-Content-Type-Options", out var headerValue1);
        response.Headers.TryGetValues("Strict-Transport-Security", out var headerValue2);
        response.Headers.TryGetValues("X-Powered-By", out var headerValue3);

        Assert.NotNull(headerValue1);
        Assert.Equal("nosniff", headerValue1.FirstOrDefault());
        
        Assert.NotNull(headerValue2);
        Assert.Equal("max-age=1800; includeSubDomains; preload", headerValue2.FirstOrDefault());
        
        Assert.Null(headerValue3);
    }

    private static async Task<IHost> CreateAndStartHostAsync(string basePath, string configFile)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configFile);
        var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer(options =>
                    {
                        options.BaseAddress = new Uri(basePath);
                    })
                    .UseConfiguration(configurationBuilder.Build())
                    .ConfigureServices(services =>
                    {
                        services.ConfigureOptions<ConfigureAppSecurityOptions>();
                    })
                    .Configure(app =>
                    {
                        app.UseMiddleware<TestServerMiddleware>();
                        app.UseSecurityHttpResponseHeaders();
                        app.Run(async context =>
                        {
                            context.Response.ContentType = "text/plain";
                            await context.Response.WriteAsync("Hello world");
                        });
                    });
            })
            .StartAsync();
        return host;
    }
}
