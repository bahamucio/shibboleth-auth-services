using Microsoft.AspNetCore.Authentication;

namespace Shiboleth.Authentication
{
    public class ShibolethAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string RemoteUser { get; set; }
    }
}