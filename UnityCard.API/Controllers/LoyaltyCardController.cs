using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.BusinessLayer.Repositories;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/loyaltycards")]
    public class LoyaltyCardController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards; // todo: remove unused
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IOfferRepository repoOffers;
        private IRetailerCategoryRepository repoRetailerCategories;
        private IRetailerLocationRepository repoRetailerLocations;
        private IRetailerRepository repoRetailers;

        public LoyaltyCardController(ILoyaltyCardRepository repoLoyaltyCards, ILoyaltyPointRepository repoLoyaltyPoints, IOfferRepository repoOffers, IRetailerCategoryRepository repoRetailerCategories, IRetailerLocationRepository repoRetailerLocations, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoOffers = repoOffers;
            this.repoRetailerCategories = repoRetailerCategories;
            this.repoRetailerLocations = repoRetailerLocations;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Retrieves the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public LoyaltyCard GetLoyaltyCard(string userId)
        {
            // GetLoyaltyCard

            return null; // todo
        }

        /// <summary>
        /// Retrieves all retailers attached to the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/retailers")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public List<Retailer> GetLoyaltyCardRetailers(string userId)
        {
            // GetLoyaltyCardRetailers

            return null; // todo
        }

        /// <summary>
        /// Add a new retailer to the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId}/retailers")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public void AddLoyaltyCardRetailer(string userId, [FromBody]int retailerId)
        {
            // AddLoyaltyCardRetailer gebruik makend van GetLoyaltyCard

            // todo
        }
    }
}
