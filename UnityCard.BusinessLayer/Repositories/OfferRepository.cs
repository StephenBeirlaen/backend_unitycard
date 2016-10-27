using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public List<Offer> GetAllRetailerOffers(string userId)
        {
            var query =
                from o in context.Offers // FROM Offers
                join r in context.Retailers // JOIN Retailers
                on o.RetailerId equals r.Id // ON Offers.RetailerId = Retailers.Id
                join lp in context.LoyaltyPoints // JOIN LoyaltyPoints
                on r.Id equals lp.RetailerId // ON Retailers.Id = LoyaltyPoints.RetailerId
                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId = LoyaltyCards.Id
                where lc.UserId == userId // WHERE LoyaltyCards.UserId=''
                select o; // SELECT Offers.*

            return query.ToList<Offer>();
        }

        public List<Offer> GetRetailerOffers(int retailerId, string userId)
        {
            var query =
                from o in context.Offers // FROM Offers
                join r in context.Retailers // JOIN Retailers
                on o.RetailerId equals r.Id // ON Offers.RetailerId = Retailers.Id
                join lp in context.LoyaltyPoints // JOIN LoyaltyPoints
                on r.Id equals lp.RetailerId // ON Retailers.Id = LoyaltyPoints.RetailerId
                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId = LoyaltyCards.Id
                where lc.UserId == userId && o.RetailerId == retailerId // WHERE LoyaltyCards.UserId='' AND Offers.RetailerId = x
                select o; // SELECT Offers.*

            return query.ToList<Offer>();
        }
    }
}