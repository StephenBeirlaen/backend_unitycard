using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IExternalAuthRepository
    {
        Task<IdentityUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(IdentityUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
    }
}