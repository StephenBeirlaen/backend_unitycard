using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface ILoyaltyCardRepository : IGenericRepository<LoyaltyCard>
    {
        Task<LoyaltyCard> GetLoyaltyCard(string userId);
        Task<LoyaltyCard> GetLoyaltyCard(string userId, DateTime lastUpdatedTimestamp);
        Task<List<Retailer>> GetLoyaltyCardRetailers(string userId, DateTime lastUpdatedTimestamp);
        Task<string> GetUserIdByLoyaltyCardId(int loyaltyCardId);
        Task<LoyaltyPoint> AddLoyaltyCardRetailer(LoyaltyCard loyaltyCard, int retailerId);
    }
}
