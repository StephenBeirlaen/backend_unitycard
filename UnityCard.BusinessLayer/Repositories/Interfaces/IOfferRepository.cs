using UnityCard.Models;
using System;
using System.Collections.Generic;
namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        List<Offer> GetAllRetailerOffers(string userId);
        List<Offer> GetRetailerOffers(int retailerId, string userId);
    }
}
