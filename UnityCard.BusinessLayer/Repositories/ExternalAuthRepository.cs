using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UnityCard.BusinessLayer.Context;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;

namespace UnityCard.BusinessLayer.Repositories
{
    public class ExternalAuthRepository : IExternalAuthRepository
    {
        private UnityCardDbContext _ctx;

        private UserManager<ApplicationUser> _userManager;

        public ExternalAuthRepository()
        {
            _ctx = new UnityCardDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
        }

        // class om te registreren of in te loggen zonder password
        // http://bitoftech.net/2014/08/11/asp-net-web-api-2-external-logins-social-logins-facebook-google-angularjs-app/

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}
