using System;
using Microsoft.AspNetCore.Authentication;

namespace Shiboleth.Authentication
{
    public static class ShibbolethAuthenticationExtensions
    {
        public static AuthenticationBuilder AddShibboleth(
            this AuthenticationBuilder builder,
            Action<ShibolethAuthenticationOptions> configureOptions)
        {
            return builder.AddShibboleth(ShibbolethAuthenticationProperties.SchemeName, configureOptions);
        }

        public static AuthenticationBuilder AddShibboleth(
            this AuthenticationBuilder builder,
            string scheme,
            Action<ShibolethAuthenticationOptions> configureOptions
        )
        {
            return builder.AddShibboleth(
                scheme, ShibbolethAuthenticationProperties.DisplayName, configureOptions);
        }

        public static AuthenticationBuilder AddShibboleth(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            string displayName,
            Action<ShibolethAuthenticationOptions> configureOptions
        )
        {
            return builder.AddScheme<ShibolethAuthenticationOptions, ShibbolethAuthenticationHandler>(
                authenticationScheme, displayName, configureOptions);
        }
    }
}