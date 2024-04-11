using Microsoft.AspNetCore.Builder;

namespace WebSecurityMiddleware;

public static class SecurityHttpResponseHeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHttpResponseHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityHttpResponseHeaderMiddleware>();
    }
}
