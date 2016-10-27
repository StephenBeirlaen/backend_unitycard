using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityCard.API.Helpers;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/loyaltypoints")]
    public class LoyaltyPointController : ApiController
    {
        public LoyaltyPointController()
        {
            
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
            // GetTotalLoyaltyPoints

            return 0; // todo
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
            // AwardLoyaltyPoints

            // todo
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
            // ModifyLoyaltyPoints

            // todo
        }
    }
}
