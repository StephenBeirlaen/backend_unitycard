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
using UnityCard.Models.ViewModels;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/loyaltycards")]
    public class LoyaltyCardController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards;
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IRetailerRepository repoRetailers;

        public LoyaltyCardController(ILoyaltyCardRepository repoLoyaltyCards, ILoyaltyPointRepository repoLoyaltyPoints, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoRetailers = repoRetailers;
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
        public async Task<List<RetailerLoyaltyPointVM>> GetLoyaltyCardRetailers(string userId, [FromUri] long lastUpdatedTimestamp)
        {
            List<RetailerLoyaltyPointVM> retailerLoyaltyPointVMs = new List<RetailerLoyaltyPointVM>();

            List<Retailer> retailers = await repoLoyaltyCards.GetLoyaltyCardRetailers(userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));
            foreach (Retailer retailer in retailers)
            {
                RetailerLoyaltyPointVM vm = new RetailerLoyaltyPointVM();
                vm.Retailer = retailer;
                vm.LoyaltyPoints = (await repoLoyaltyPoints.GetLoyaltyPoint(userId, retailer.Id)).Points;

                retailerLoyaltyPointVMs.Add(vm);
            }

            return retailerLoyaltyPointVMs;
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

            HttpResponseMessage response;

            List<Retailer> existingAddedRetailers = await repoLoyaltyCards.GetLoyaltyCardRetailers(userId, new DateTime(0));

            if (existingAddedRetailers.Any(r => r.Id == body.RetailerId)) // werd de retailer al eens toegevoegd?
            { // de retailer werd al eens toegevoegd
                response = Request.CreateResponse(HttpStatusCode.OK); // geen error geven, is geen probleem
            }
            else
            {
                LoyaltyCard loyaltyCard = await repoLoyaltyCards.GetLoyaltyCard(userId);

                LoyaltyPoint loyaltyPoint = await repoLoyaltyCards.AddLoyaltyCardRetailer(loyaltyCard, body.RetailerId);

                if (loyaltyPoint != null)
                {
                    await repoRetailers.RenewRetailerUpdatedTimestamp(body.RetailerId);

                    response = Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            
            return response;
        }
    }
}
