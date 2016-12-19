using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class LoyaltyPointRepository : GenericRepository<LoyaltyPoint>, ILoyaltyPointRepository
    {
        public async Task<int> GetTotalLoyaltyPoints(string userId, DateTime lastUpdatedTimestamp)
        {
            int result = await
                (from lp in context.LoyaltyPoints // FROM LoyaltyPoints
                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId=LoyaltyCards.Id
                where lc.UserId == userId // WHERE LoyaltyCards.UserId = 'userid'
                && lp.UpdatedTimestamp > lastUpdatedTimestamp
                select lp.Points).SumAsync(); // SELECT SUM(LoyaltyPoints.Value)
            
            return result;
        }

        public async Task<LoyaltyPoint> GetLoyaltyPoint(string userId, int retailerId)
        {
            var loyaltyPoint = await (from lp in context.LoyaltyPoints // FROM LoyaltyPoints
                                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId = LoyaltyCards.Id
                                where lc.UserId == userId && lp.RetailerId == retailerId   // WHERE LoyaltyCards.UserId = '' AND LoyaltyPoints.RetailerId = x
                                select lp).SingleOrDefaultAsync(); // UPDATE LoyaltyPoints

            return loyaltyPoint;
        }

        public async Task<LoyaltyPoint> AwardLoyaltyPoints(string userId, int retailerId, int loyaltyPointsIncrementAmount)
        {
            LoyaltyPoint loyaltyPoint = await GetLoyaltyPoint(userId, retailerId);

            if (loyaltyPoint != null)
            {
                loyaltyPoint.Points += loyaltyPointsIncrementAmount; // SET LoyaltyPoints.Points = LoyaltyPoints.Points + x
                loyaltyPoint.UpdatedTimestamp = DateTime.UtcNow;

                try
                {
                    await SaveChanges();
                }
                catch (Exception e)
                {
                    return null;
                }

                return loyaltyPoint;
            }

            return null;
        }

        public async Task<LoyaltyPoint> ModifyLoyaltyPoints(string userId, int retailerId, int loyaltyPointsCount)
        {
            LoyaltyPoint loyaltyPoint = await GetLoyaltyPoint(userId, retailerId);

            if (loyaltyPoint != null)
            {
                loyaltyPoint.Points = loyaltyPointsCount; // SET LoyaltyPoints.Points = x
                loyaltyPoint.UpdatedTimestamp = DateTime.UtcNow;

                try
                {
                    await SaveChanges();
                }
                catch (Exception e)
                {
                    return null;
                }

                return loyaltyPoint;
            }

            return null;
        }
    }
}