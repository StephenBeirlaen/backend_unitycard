using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.Models;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/offers")]
    public class OfferController : ApiController
    {
        public OfferController()
        {
            
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
            // GetAllRetailerOffers

            return null; // todo
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
            // GetRetailerOffers

            return null; // todo
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
            // OfferRepository.Insert (uit generic repo klasse)

            // todo
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
