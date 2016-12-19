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
    [RoutePrefix("api/loyaltypoints")]
    public class LoyaltyPointController : ApiController
    {
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IRetailerRepository repoRetailers;

        public LoyaltyPointController(ILoyaltyPointRepository repoLoyaltyPoints, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Get total amount of loyalty points collected
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<int> GetTotalLoyaltyPoints(string userId, [FromUri] long lastUpdatedTimestamp)
        {
            int totalLoyaltyPoints = await repoLoyaltyPoints.GetTotalLoyaltyPoints(userId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return totalLoyaltyPoints;
        }

        /// <summary>
        /// Award an amount of loyalty points to the specified user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}/{retailerId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<LoyaltyPoint> GetLoyaltyPoint(string userId, int retailerId)
        {
            LoyaltyPoint loyaltyPoints = await repoLoyaltyPoints.GetLoyaltyPoint(userId, retailerId);

            return loyaltyPoints;
        }

        /// <summary>
        /// Award an amount of loyalty points to the specified user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{userId}/{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> AwardLoyaltyPoints(string userId, int retailerId, AwardLoyaltyPointsBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            LoyaltyPoint loyaltyPoint = await repoLoyaltyPoints.AwardLoyaltyPoints(userId, retailerId, body.LoyaltyPointsIncrementAmount);

            HttpResponseMessage response;

            if (loyaltyPoint != null)
            {
                await repoRetailers.RenewRetailerUpdatedTimestamp(retailerId);

                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        /// <summary>
        /// Change the specified users's loyalty points
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{userId}/{retailerId}")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> ModifyLoyaltyPoints(string userId, int retailerId, ModifyLoyaltyPointsBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            LoyaltyPoint loyaltyPoint = await repoLoyaltyPoints.ModifyLoyaltyPoints(userId, retailerId, body.LoyaltyPointsCount);

            HttpResponseMessage response;

            if (loyaltyPoint != null)
            {
                await repoRetailers.RenewRetailerUpdatedTimestamp(retailerId);

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
