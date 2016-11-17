using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.BusinessLayer.Repositories;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;
using UnityCard.Models.PostModels;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/loyaltycards")]
    public class LoyaltyCardController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards;

        public LoyaltyCardController(ILoyaltyCardRepository repoLoyaltyCards)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
        }

        /// <summary>
        /// Retrieves the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<LoyaltyCard> GetLoyaltyCard(string userId, [FromUri] long lastUpdatedTimestamp)
        {
            LoyaltyCard loyaltycard = await repoLoyaltyCards.GetLoyaltyCard(userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return loyaltycard;
        }

        /// <summary>
        /// Retrieves all retailers attached to the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/retailers")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<List<Retailer>> GetLoyaltyCardRetailers(string userId, [FromUri] long lastUpdatedTimestamp)
        {
            List<Retailer> retailers = await repoLoyaltyCards.GetLoyaltyCardRetailers(userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return retailers;
        }

        /// <summary>
        /// Add a new retailer to the specified user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId}/retailers")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<HttpResponseMessage> AddLoyaltyCardRetailer(string userId, AddLoyaltyCardRetailerBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            LoyaltyCard loyaltyCard = await repoLoyaltyCards.GetLoyaltyCard(userId);

            LoyaltyPoint loyaltyPoint = await repoLoyaltyCards.AddLoyaltyCardRetailer(loyaltyCard, body.RetailerId);

            HttpResponseMessage response;

            if (loyaltyPoint != null)
            {
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
            return response;
        }
    }
}
