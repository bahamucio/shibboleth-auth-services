using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Shiboleth.Authentication
{
    public class ShibbolethAuthenticationHandler : AuthenticationHandler<ShibolethAuthenticationOptions>
    {
        private readonly IUserRepository _userRepository;

        public ShibbolethAuthenticationHandler(
            IOptionsMonitor<ShibolethAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userRepository)
                : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var remoteUser = Request.Headers
                .FirstOrDefault(h => h.Key == Options.RemoteUser).Value;

            var userInfo = await _userRepository.FindAsync(remoteUser);

            if(userInfo == null)
            {
                return AuthenticateResult.Fail("Unknown user.");
            }

            var user = CreateUser(userInfo);

            return AuthenticateResult.Success(
                new AuthenticationTicket(user, new AuthenticationProperties(), Scheme.Name)
            );
        }

        private ClaimsPrincipal CreateUser(UserInfo userInfo)
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(Claims.UserName, userInfo.UserName));
            return new ClaimsPrincipal(identity);
        }

        private static class Claims
        {
            public static string UserName => "username";
        }
    }
}
