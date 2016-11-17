using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UnityCard.BusinessLayer.Repositories.Interfaces;

namespace UnityCard.BusinessLayer.Repositories
{
    public class RetailerLocationRepository : GenericRepository<RetailerLocation>, IRetailerLocationRepository
    {
        public async Task<List<RetailerLocation>> GetRetailerLocations(int retailerId, DateTime lastUpdatedTimestamp)
        {
            var query =
                from rl in context.RetailerLocations // FROM RetailerLocations
                where rl.RetailerId == retailerId // WHERE RetailerLocations.RetailerId = retailerId
                && rl.UpdatedTimestamp > lastUpdatedTimestamp
                select rl; // SELECT RetailerLocations.*

            return await query.ToListAsync<RetailerLocation>();
        }

        public async Task<RetailerLocation> GetRetailerLocation(int locationId)
        {
            var query =
                from rl in context.RetailerLocations // FROM RetailerLocations
                where rl.Id == locationId // WHERE RetailerLocations.Id = locationId
                select rl; // SELECT RetailerLocations.*

            return await query.SingleOrDefaultAsync();
        }
    }
}