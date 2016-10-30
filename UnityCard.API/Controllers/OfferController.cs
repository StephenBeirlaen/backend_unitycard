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
    [RoutePrefix("api/offers")]
    public class OfferController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards; // todo: remove unused
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IOfferRepository repoOffers;
        private IRetailerCategoryRepository repoRetailerCategories;
        private IRetailerLocationRepository repoRetailerLocations;
        private IRetailerRepository repoRetailers;

        public OfferController(ILoyaltyCardRepository repoLoyaltyCards, ILoyaltyPointRepository repoLoyaltyPoints, IOfferRepository repoOffers, IRetailerCategoryRepository repoRetailerCategories, IRetailerLocationRepository repoRetailerLocations, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoOffers = repoOffers;
            this.repoRetailerCategories = repoRetailerCategories;
            this.repoRetailerLocations = repoRetailerLocations;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Retrieves all relevant offers according to a users loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public List<Offer> GetAllRetailerOffers(string userId)
        {
            List<Offer> lijstOffers = repoOffers.GetAllRetailerOffers(userId);

            return lijstOffers;
        }

        /// <summary>
        /// Retrieves all relevant offers of a specific retailer according to the user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public List<Offer> GetRetailerOffers(int retailerId, string userId)
        {
            List<Offer> lijstOffersByRetailer = repoOffers.GetRetailerOffers(retailerId, userId);

            return lijstOffersByRetailer;
        }

        /// <summary>
        /// Add an offer for a specific retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void AddRetailerOffer(
            int retailerId,
            [FromBody]string offerDemand,
            [FromBody]string offerReceive)
        {
            Offer offer = new Offer(retailerId, offerDemand, offerReceive);

            repoOffers.Insert(offer);
        }

        /// <summary>
        /// Push a new ad notification to all clients of a retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}/push")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void PushAdvertisementNotification(
            int retailerId,
            [FromBody]string title,
            [FromBody]string description)
        {
            // todo GCM (uitbreiding voor later)
        }
    }
}
