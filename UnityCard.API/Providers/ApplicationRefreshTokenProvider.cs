using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using UnityCard.Models;

namespace UnityCard.API.Providers
{
    public class ApplicationRefreshTokenProvider : AuthenticationTokenProvider
    {
        private readonly int _tokenExpiration;

        public ApplicationRefreshTokenProvider()
        {
            _tokenExpiration = 10; // 10 days
        }

        public override void Create(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.ExpiresUtc = new DateTimeOffset(DateTime.Now.AddDays(_tokenExpiration));
            context.SetToken(context.SerializeTicket());
        }

        public override void Receive(AuthenticationTokenReceiveContext context)
        {
            context.DeserializeTicket(context.Token);
        }
    }
}