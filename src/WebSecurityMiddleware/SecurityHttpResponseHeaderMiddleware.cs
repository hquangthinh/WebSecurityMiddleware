using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace WebSecurityMiddleware;

public class SecurityHttpResponseHeaderMiddleware(RequestDelegate next, IOptions<AppSecurityConfiguration> options)
{
    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;
        
        response.OnStarting(() =>
        {
            foreach (var header in options.Value.HttpResponseHeaders.Add)
                response.Headers.Append(header.Key, header.Value);
            
            foreach (var header in options.Value.HttpResponseHeaders.Remove)
                response.Headers.Remove(header);
            
            return Task.CompletedTask;
        });
        
        await next(context);
    }
}
