using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class LoyaltyPointRepository : GenericRepository<LoyaltyPoint>, ILoyaltyPointRepository
    {
        public int GetTotalLoyaltyPoints(string userId)
        {
            int result =
                (from lp in context.LoyaltyPoints // FROM LoyaltyPoints
                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId=LoyaltyCards.Id
                where lc.UserId == userId // WHERE LoyaltyCards.UserId = 'userid'
                select lp.Points).Sum(); // SELECT SUM(LoyaltyPoints.Value)
            
            return result;
        }

        public LoyaltyPoint GetLoyaltyPoint(string userId, int retailerId)
        {
            var loyaltyPoint = (from lp in context.LoyaltyPoints // FROM LoyaltyPoints
                                join lc in context.LoyaltyCards // JOIN LoyaltyCards
                                on lp.LoyaltyCardId equals lc.Id // ON LoyaltyPoints.LoyaltyCardId = LoyaltyCards.Id
                                where lc.UserId == userId && lp.RetailerId == retailerId   // WHERE LoyaltyCards.UserId = '' AND LoyaltyPoints.RetailerId = x
                                select lp).SingleOrDefault(); // UPDATE LoyaltyPoints

            return loyaltyPoint;
        }

        public LoyaltyPoint AwardLoyaltyPoints(string userId, int retailerId, int loyaltyPointsIncrementAmount)
        {
            LoyaltyPoint loyaltyPoint = GetLoyaltyPoint(userId, retailerId);

            if (loyaltyPoint != null)
            {
                loyaltyPoint.Points += loyaltyPointsIncrementAmount; // SET LoyaltyPoints.Points = LoyaltyPoints.Points + x

                SaveChanges();

                return loyaltyPoint;
            }

            return null;
        }

        public LoyaltyPoint ModifyLoyaltyPoints(string userId, int retailerId, int loyaltyPointsCount)
        {
            LoyaltyPoint loyaltyPoint = GetLoyaltyPoint(userId, retailerId);

            if (loyaltyPoint != null)
            {
                loyaltyPoint.Points = loyaltyPointsCount; // SET LoyaltyPoints.Points = x

                SaveChanges();

                return loyaltyPoint;
            }

            return null;
        }
    }
}