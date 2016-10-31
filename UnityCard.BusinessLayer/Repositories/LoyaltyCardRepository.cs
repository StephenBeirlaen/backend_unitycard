using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class LoyaltyCardRepository : GenericRepository<LoyaltyCard>, ILoyaltyCardRepository
    {
        public async Task<LoyaltyCard> GetLoyaltyCard(string userId)
        {
            var query = (from lc in context.LoyaltyCards.Include(lc => lc.LoyaltyPoints)
                         where lc.UserId == userId
                         select lc);
            return await query.SingleOrDefaultAsync();
        }

        public async Task<List<Retailer>> GetLoyaltyCardRetailers(string userId)
        {
            var query =
                from lc in context.LoyaltyCards // FROM LoyaltyCards
                join lp in context.LoyaltyPoints // JOIN LoyaltyPoints
                on lc.Id equals lp.LoyaltyCardId // ON LoyaltyCards.Id=LoyaltyPoints.LoyaltyCardId
                join r in context.Retailers // JOIN Retailers
                on lp.RetailerId equals r.Id // ON LoyaltyPoints.RetailerId = Retailers.Id
                where lc.UserId == userId // WHERE LoyaltyCards.UserId = 'userid'
                select r; // SELECT Retailers.*

            return await query.ToListAsync<Retailer>();
        }

        public async Task<LoyaltyPoint> AddLoyaltyCardRetailer(LoyaltyCard loyaltyCard, int retailerId)
        {
            LoyaltyPoint newLoyaltyPoint = new LoyaltyPoint(loyaltyCard.Id, retailerId, 0);

            context.LoyaltyPoints.Add(newLoyaltyPoint);

            try
            {
                await SaveChanges();
            }
            catch (Exception e)
            {
                return null;
            }

            return newLoyaltyPoint; // om de id terug te geven nadat het in de db werd gestoken
        }
    }
}