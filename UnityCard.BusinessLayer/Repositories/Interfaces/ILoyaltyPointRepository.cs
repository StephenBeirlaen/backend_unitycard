using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface ILoyaltyPointRepository : IGenericRepository<LoyaltyPoint>
    {
        Task<int> GetTotalLoyaltyPoints(string userId, DateTime lastUpdatedTimestamp);
        Task<LoyaltyPoint> GetLoyaltyPoint(string userId, int retailerId);
        Task<LoyaltyPoint> AwardLoyaltyPoints(string userId, int retailerId, int loyaltyPointsIncrementAmount);
        Task<LoyaltyPoint> ModifyLoyaltyPoints(string userId, int retailerId, int loyaltyPointsCount);
    }
}
