using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface ILoyaltyCardRepository : IGenericRepository<LoyaltyCard>
    {
        Task<LoyaltyCard> GetLoyaltyCard(string userId);
        Task<List<Retailer>> GetLoyaltyCardRetailers(string userId);
        Task<LoyaltyPoint> AddLoyaltyCardRetailer(LoyaltyCard loyaltyCard, int retailerId);
    }
}
