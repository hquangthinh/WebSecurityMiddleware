namespace WebSecurityMiddleware;

public class AppSecurityConfiguration
{
    public const string ConfigSectionName = "AppSecurity";

    public HttpResponseHeaderConfiguration HttpResponseHeaders { get; set; } = new();
}
