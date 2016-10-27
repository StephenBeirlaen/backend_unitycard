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
    [RoutePrefix("api/retailers")]
    public class RetailerController : ApiController
    {
        private ILoyaltyCardRepository repoLoyaltyCards; // todo: remove unused
        private ILoyaltyPointRepository repoLoyaltyPoints;
        private IOfferRepository repoOffers;
        private IRetailerCategoryRepository repoRetailerCategories;
        private IRetailerLocationRepository repoRetailerLocations;
        private IRetailerRepository repoRetailers;

        public RetailerController(ILoyaltyCardRepository repoLoyaltyCards, ILoyaltyPointRepository repoLoyaltyPoints, IOfferRepository repoOffers, IRetailerCategoryRepository repoRetailerCategories, IRetailerLocationRepository repoRetailerLocations, IRetailerRepository repoRetailers)
        {
            this.repoLoyaltyCards = repoLoyaltyCards;
            this.repoLoyaltyPoints = repoLoyaltyPoints;
            this.repoOffers = repoOffers;
            this.repoRetailerCategories = repoRetailerCategories;
            this.repoRetailerLocations = repoRetailerLocations;
            this.repoRetailers = repoRetailers;
        }

        /// <summary>
        /// Retrieves all retailers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public IEnumerable<string> GetAllRetailers()
        {
            // RetailerRepository.GetAll (uit generic repo klasse)
            // todo: test include sub objecten van retailers?

            return null; // todo
        }

        /// <summary>
        /// Add a retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void AddRetailer(
            [FromBody]string retailerName,
            [FromBody]string retailerTagline,
            [FromBody]string retailerLogoUrl,
            [FromBody]int retailerCategoryId,
            [FromBody]bool isChain)
        {
            // RetailerRepository.Insert (uit generic repo klasse)

            // todo
        }

        /// <summary>
        /// Retrieves a specific retailer and its details: aantal punten verzameld, enkele winkel of keten, indien keten hoeveel winkels en locatie dichtstbijzijnde winkel, indien geen keten locatie winkel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public Retailer GetRetailer(int retailerId)
        {
            // RetailerRepository.GetByID (uit generic repo klasse)
            // todo: test include sub objecten van retailer?

            return null; // todo
        }

        /// <summary>
        /// Retrieves all locations of a retailer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/locations")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public List<RetailerLocation> GetRetailerLocations(int retailerId)
        {
            // GetRetailerLocations

            return null; // todo
        }

        /// <summary>
        /// Retrieves details of a specific location of a retailer (chain)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/locations/{locationId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public RetailerLocation GetRetailerLocation(int retailerId, int locationId)
        {
            // GetRetailerLocation (enkel locationId nodig)

            return null; // todo
        }

        /// <summary>
        /// Add a location of a retailer (chain)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}/locations")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public void AddRetailerLocation(
            int retailerId,
            [FromBody]string name,
            [FromBody]double latitude,
            [FromBody]double longitude,
            [FromBody]string street,
            [FromBody]string number,
            [FromBody]int zipCode,
            [FromBody]string city,
            [FromBody]string country)
        {
            // RetailerLocationRepository.Insert (uit generic repo klasse)

            // todo
        }

        /// <summary>
        /// Retrieves all retailer categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public IEnumerable<string> GetAllRetailerCategories()
        {
            // RetailerCategoryRepository.GetAll (uit generic repo klasse)

            return null; // todo
        }
    }
}
