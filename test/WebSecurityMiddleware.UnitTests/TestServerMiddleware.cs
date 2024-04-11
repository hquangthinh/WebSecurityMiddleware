using Microsoft.AspNetCore.Http;

namespace WebSecurityMiddleware.UnitTests;

internal class TestServerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Append("Server", "TestServer");
        context.Response.Headers.Append("X-Powered-By", "Dotnet");
        context.Response.Headers.Append("X-AspNet-Version", "8");

        await next(context);
    }
}
