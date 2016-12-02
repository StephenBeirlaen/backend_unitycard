using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using UnityCard.API.Helpers;
using UnityCard.BusinessLayer.Repositories.Interfaces;
using UnityCard.Models;
using UnityCard.Models.PostModels;

namespace UnityCard.API.Controllers
{
    [RoutePrefix("api/retailers")]
    public class RetailerController : ApiController
    {
        private IRetailerCategoryRepository repoRetailerCategories;
        private IRetailerLocationRepository repoRetailerLocations;
        private IRetailerRepository repoRetailers;

        public RetailerController(IRetailerCategoryRepository repoRetailerCategories, IRetailerLocationRepository repoRetailerLocations, IRetailerRepository repoRetailers)
        {
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
        public async Task<List<Retailer>> GetAllRetailers([FromUri] long lastUpdatedTimestamp)
        {
            List<Retailer> retailers = (await repoRetailers.GetAllRetailers(TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp))).ToList();

            return retailers;
        }

        /// <summary>
        /// Add a retailer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> AddRetailer(AddRetailerBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Retailer retailer = new Retailer(
                body.RetailerCategoryId,
                body.RetailerName,
                body.RetailerTagline,
                body.IsChain,
                body.RetailerLogoUrl);

            repoRetailers.Insert(retailer);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                await repoRetailers.SaveChanges();
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        /// <summary>
        /// Retrieves a specific retailer and its details: aantal punten verzameld, enkele winkel of keten, indien keten hoeveel winkels en locatie dichtstbijzijnde winkel, indien geen keten locatie winkel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<Retailer> GetRetailer(int retailerId)
        {
            Retailer retailer = await repoRetailers.GetByID(retailerId);

            return retailer;
        }

        /// <summary>
        /// Retrieves all locations of a retailer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/locations")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<List<RetailerLocation>> GetRetailerLocations(int retailerId, [FromUri] long lastUpdatedTimestamp)
        {
            List<RetailerLocation> retailerLocations = await repoRetailerLocations.GetRetailerLocations(retailerId, TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return retailerLocations;
        }

        /// <summary>
        /// Retrieves details of a specific location of a retailer (chain)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{retailerId}/locations/{locationId}")]
        [Authorize(Roles = ApplicationRoles.CUSTOMER)]
        public async Task<RetailerLocation> GetRetailerLocation(int retailerId, int locationId)
        {
            RetailerLocation retailerLocation = await repoRetailerLocations.GetRetailerLocation(locationId);

            return retailerLocation;
        }

        /// <summary>
        /// Add a location of a retailer (chain)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("{retailerId}/locations")]
        [Authorize(Roles = ApplicationRoles.RETAILER)]
        public async Task<HttpResponseMessage> AddRetailerLocation(int retailerId, AddRetailerLocationBody body)
        {
            if (body == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            RetailerLocation retailerLocation = new RetailerLocation(
                retailerId,
                body.Name,
                body.Street,
                body.Number,
                body.ZipCode,
                body.City,
                body.Country);

            repoRetailerLocations.Insert(retailerLocation);
            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            try
            {
                await repoRetailerLocations.SaveChanges();
            }
            catch (Exception e)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        /// <summary>
        /// Retrieves all retailer categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public async Task<List<RetailerCategory>> GetAllRetailerCategories([FromUri] long lastUpdatedTimestamp)
        {
            List<RetailerCategory> retailerCategories = await repoRetailerCategories.GetAllRetailerCategories(TimestampHelper.UnixTimeStampToDateTime(lastUpdatedTimestamp));

            return retailerCategories;
        }
    }
}
