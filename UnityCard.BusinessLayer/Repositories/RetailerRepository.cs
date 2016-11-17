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
    public class RetailerRepository : GenericRepository<Retailer>, IRetailerRepository
    {
        public async Task<List<Retailer>> GetAllRetailers(DateTime lastUpdatedTimestamp)
        {
            var query = (from r in context.Retailers
                         where r.UpdatedTimestamp > lastUpdatedTimestamp
                         select r);
            return await query.ToListAsync();
        }
    }
}