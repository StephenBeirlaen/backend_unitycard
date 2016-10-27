using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class RetailerLocationRepository : GenericRepository<RetailerLocation>, IRetailerLocationRepository
    {
        public List<RetailerLocation> GetRetailerLocations(int retailerId)
        {
            var query =
                from rl in context.RetailerLocations // FROM RetailerLocations
                where rl.RetailerId == retailerId // WHERE RetailerLocations.RetailerId = retailerId
                select rl; // SELECT RetailerLocations.*

            // todo: include sub objecten van retailerlocations?

            return query.ToList<RetailerLocation>();
        }

        public RetailerLocation GetRetailerLocation(int locationId)
        {
            var query =
                from rl in context.RetailerLocations // FROM RetailerLocations
                where rl.Id == locationId // WHERE RetailerLocations.Id = locationId
                select rl; // SELECT RetailerLocations.*

            // todo: include sub objecten van retailerlocation?

            return query.SingleOrDefault();
        }
    }
}