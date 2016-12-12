using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UnityCard.Models;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IExternalAuthRepository
    {
        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}