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
    public class RetailerRepository : GenericRepository<Retailer>, IRetailerRepository
    {
        public async Task<List<Retailer>> GetAllRetailers(DateTime lastUpdatedTimestamp)
        {
            var query = (from r in context.Retailers
                         where r.UpdatedTimestamp > lastUpdatedTimestamp
                         select r);
            return await query.ToListAsync();
        }

        public async Task<List<String>> GetAllUserFcmTokensAttachedToRetailer(int retailerId)
        {
            var query = (from r in context.Retailers
                         where r.Id == retailerId
                         join lp in context.LoyaltyPoints
                         on r.Id equals lp.RetailerId
                         join lc in context.LoyaltyCards
                         on lp.LoyaltyCardId equals lc.Id
                         join au in context.Users
                         on lc.UserId equals au.Id
                         select au.FirebaseCloudMessagingRegistrationToken);
            return await query.ToListAsync();
        }

        public async Task RenewRetailerUpdatedTimestamp(int retailerId)
        {
            Retailer retailer = await GetByID(retailerId);
            if (retailer != null)
            {
                retailer.UpdatedTimestamp = DateTime.UtcNow;
                await SaveChanges();
            }
        }
    }
}