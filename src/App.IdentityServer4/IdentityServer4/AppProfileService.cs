using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.IdentityServer4
{
    public class AppProfileService : IProfileService
    {
        private readonly ILogger _logger;
        public AppProfileService(ILogger<AppProfileService> logger)
        {
            _logger = logger;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, "yannis@live.com"),
                    new Claim(ClaimTypes.GivenName, "yannis"),
                    new Claim(ClaimTypes.Name, "yannisName"),
                };
            context.IssuedClaims = claims;
            await Task.FromResult(0);
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            _logger.LogDebug("IsActive called from: {caller}", context.Caller);

            context.IsActive = true;
            await Task.FromResult(0);
        }
    }
}
