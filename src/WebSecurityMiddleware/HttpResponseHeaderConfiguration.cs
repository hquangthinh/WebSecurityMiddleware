using System.Collections.Generic;

namespace WebSecurityMiddleware;

public class HttpResponseHeaderConfiguration
{
    public Dictionary<string, string> Add { get; set; } = [];
    public string[] Remove { get; set; } = [];
}
