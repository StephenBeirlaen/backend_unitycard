using UnityCard.Models;
using System;
using System.Collections.Generic;
namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface ILoyaltyCardRepository : IGenericRepository<LoyaltyCard>
    {
        LoyaltyCard GetLoyaltyCard(string userId);
        List<Retailer> GetLoyaltyCardRetailers(string userId);
        Retailer AddLoyaltyCardRetailer(LoyaltyCard loyaltyCard, Retailer retailer);
    }
}
