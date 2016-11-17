using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<List<Offer>> GetAllRetailerOffers(string userId, DateTime lastUpdatedTimestamp);
        Task<List<Offer>> GetRetailerOffers(int retailerId, string userId, DateTime lastUpdatedTimestamp);
    }
}
