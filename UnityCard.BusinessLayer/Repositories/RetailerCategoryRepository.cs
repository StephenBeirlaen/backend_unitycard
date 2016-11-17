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
    public class RetailerCategoryRepository : GenericRepository<RetailerCategory>, IRetailerCategoryRepository
    {
        public async Task<List<RetailerCategory>> GetAllRetailerCategories(DateTime lastUpdatedTimestamp)
        {
            var query = (from rc in context.RetailerCategories
                         where rc.UpdatedTimestamp > lastUpdatedTimestamp
                         select rc);
            return await query.ToListAsync();
        }
    }
}