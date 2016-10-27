using UnityCard.Models;
using System;
using System.Collections.Generic;
namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface ILoyaltyPointRepository : IGenericRepository<LoyaltyPoint>
    {
        int GetTotalLoyaltyPoints(string userId);
        LoyaltyPoint GetLoyaltyPoint(string userId, int retailerId);
        LoyaltyPoint AwardLoyaltyPoints(string userId, int retailerId, int loyaltyPointsIncrementAmount);
        LoyaltyPoint ModifyLoyaltyPoints(string userId, int retailerId, int loyaltyPointsCount);
    }
}
