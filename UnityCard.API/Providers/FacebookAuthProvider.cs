using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.Facebook;

namespace UnityCard.API.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            //context.Identity.AddClaim(new Claim("Email", context.Email));
            //context.Identity.AddClaim(new Claim("FirstName", context.Name));
            //context.Identity.AddClaim(new Claim("LastName", ""));

            return Task.FromResult<object>(null);
        }
    }
}