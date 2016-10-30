using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/loyaltypoints")]
    public class LoyaltyPointController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards; // todo: remove unused
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IOfferRepository repoOffers;
        private IRetailerCategoryRepository repoRetailerCategories;
        private IRetailerLocationRepository repoRetailerLocations;
        private IRetailerRepository repoRetailers;

        public LoyaltyPointController(ILoyaltyCardRepository repoLoyaltyCards, ILoyaltyPointRepository repoLoyaltyPoints, IOfferRepository repoOffers, IRetailerCategoryRepository repoRetailerCategories, IRetailerLocationRepository repoRetailerLocations, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoOffers = repoOffers;
            this.repoRetailerCategories = repoRetailerCategories;
            this.repoRetailerLocations = repoRetailerLocations;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Get total amount of loyalty points collected
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public int GetTotalLoyaltyPoints(string userId)
        {
            int totalLoyaltyPoints = repoLoyaltyPoints.GetTotalLoyaltyPoints(userId);

            return totalLoyaltyPoints;
        }

        /// <summary>
        /// Award an amount of loyalty points to the specified user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId}/{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void AwardLoyaltyPoints(string userId, int retailerId, [FromBody]int loyaltyPointsIncrementAmount)
        {
            repoLoyaltyPoints.AwardLoyaltyPoints(userId, retailerId, loyaltyPointsIncrementAmount);
        }

        /// <summary>
        /// Change the specified users's loyalty points
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{userId}/{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void ModifyLoyaltyPoints(string userId, int retailerId, [FromBody]int loyaltyPointsCount)
        {

            repoLoyaltyPoints.ModifyLoyaltyPoints(userId, retailerId, loyaltyPointsCount);
        }
    }
}
