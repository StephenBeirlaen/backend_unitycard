using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class LoyaltyCardRepository : GenericRepository<LoyaltyCard>, ILoyaltyCardRepository
    {
        public LoyaltyCard GetLoyaltyCard(string userId)
        {
            var query = (from lc in context.LoyaltyCards.Include(lc => lc.LoyaltyPoints)
                         where lc.UserId == userId
                         select lc);
            return query.SingleOrDefault();

            // todo: test include sub objecten van retailers?
        }

        public List<Retailer> GetLoyaltyCardRetailers(string userId)
        {
            var query =
                from lc in context.LoyaltyCards // FROM LoyaltyCards
                join lp in context.LoyaltyPoints // JOIN LoyaltyPoints
                on lc.Id equals lp.LoyaltyCardId // ON LoyaltyCards.Id=LoyaltyPoints.LoyaltyCardId
                join r in context.Retailers // JOIN Retailers
                on lp.RetailerId equals r.Id // ON LoyaltyPoints.RetailerId = Retailers.Id
                where lc.UserId == userId // WHERE LoyaltyCards.UserId = 'userid'
                select r; // SELECT Retailers.*

            return query.ToList<Retailer>();
        }

        public Retailer AddLoyaltyCardRetailer(LoyaltyCard loyaltyCard, Retailer retailer)
        {
            context.Entry<RetailerCategory>(retailer.RetailerCategory).State = EntityState.Unchanged;
            
            foreach (LoyaltyPoint loyaltyPoint in retailer.LoyaltyPoints)
            {
                context.Entry<LoyaltyPoint>(loyaltyPoint).State = EntityState.Unchanged;
            }
            foreach (RetailerLocation retailerLocation in retailer.RetailerLocations)
            {
                context.Entry<RetailerLocation>(retailerLocation).State = EntityState.Unchanged;
            }
            foreach (Offer offer in retailer.Offers)
            {
                context.Entry<Offer>(offer).State = EntityState.Unchanged;
            }

            context.Retailers.Add(retailer);

            SaveChanges();

            LoyaltyPoint newLoyaltyPoint = new LoyaltyPoint(loyaltyCard.Id, retailer.Id, 0);

            context.LoyaltyPoints.Add(newLoyaltyPoint);

            SaveChanges(); // todo: try to merge two requests into one! door retailer in newloyaltypoint te steken?

            return retailer; // om de id terug te geven nadat het in de db werd gestoken
        }
    }
}