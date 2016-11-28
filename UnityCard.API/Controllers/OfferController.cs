using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;
using UnityCard.Models.PostModels;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/offers")]
    public class OfferController : ApiController
    {
        private IOfferRepository repoOffers;
        private IRetailerRepository repoRetailers;

        public OfferController(IOfferRepository repoOffers, IRetailerRepository repoRetailers)
        {
            this.repoOffers = repoOffers;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Retrieves all relevant offers according to a users loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<List<Offer>> GetAllRetailerOffers(string userId, [FromUri] long lastUpdatedTimestamp)
        {
            List<Offer> offers = await repoOffers.GetAllRetailerOffers(userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return offers;
        }

        /// <summary>
        /// Retrieves all relevant offers of a specific retailer according to the user's loyalty card
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<List<Offer>> GetRetailerOffers(int retailerId, string userId, [FromUri] long lastUpdatedTimestamp)
        {
            List<Offer> offersByRetailer = await repoOffers.GetRetailerOffers(retailerId, userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return offersByRetailer;
        }

        /// <summary>
        /// Add an offer for a specific retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> AddRetailerOffer(int retailerId, AddRetailerOfferBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Offer offer = new Offer(retailerId, body.OfferDemand, body.OfferReceive);

            repoOffers.Insert(offer);
            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                await repoOffers.SaveChanges();
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        /// <summary>
        /// Push a new ad notification to all clients of a retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}/push")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> PushAdvertisementNotification(int retailerId, PushAdvertisementNotificationBody body)
        {
            if (body != null)
            {
                Retailer retailer = await repoRetailers.GetByID(retailerId);

                if (retailer != null)
                {
                    AdvertisementNotification advertisementNotification = new AdvertisementNotification(retailerId, body.Title);
                    GcmHelper.SendNotification(
                        retailerId,
                        retailer.Name,
                        body.Title);

                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
