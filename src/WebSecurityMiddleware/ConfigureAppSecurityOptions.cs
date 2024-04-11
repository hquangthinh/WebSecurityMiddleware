using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace WebSecurityMiddleware;

public class ConfigureAppSecurityOptions(IConfiguration configuration)
    : IConfigureOptions<AppSecurityConfiguration>
{
    public void Configure(AppSecurityConfiguration options)
    {
        configuration.Bind(AppSecurityConfiguration.ConfigSectionName, options);
    }
}
