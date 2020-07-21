using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Masuit.Tools;

namespace App.Core.Security
{
    public static class ClaimsIdentityExtensions
    {
        public static int? FindUserId(this ClaimsPrincipal principal)
        {
            Claim userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdOrNull == null || userIdOrNull.Value.IsNullOrEmpty())
            {
                return null;
            }

            return int.Parse(userIdOrNull.Value);
        }

        public static string FindUserName(this ClaimsPrincipal principal)
        {
            Claim userNameOrNull = principal.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return userNameOrNull?.Value;
        }

    }
}
