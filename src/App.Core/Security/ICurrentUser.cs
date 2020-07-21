﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace App.Core.Security
{
    public interface ICurrentUser
    {
        long? Id { get; }
        string UserName { get; }
        Claim FindClaim(string claimType);
        Claim[] FindClaims(string claimType);
        Claim[] GetAllClaims();

    }
}
