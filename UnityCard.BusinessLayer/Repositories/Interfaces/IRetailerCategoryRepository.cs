using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IRetailerCategoryRepository : IGenericRepository<RetailerCategory>
    {
        Task<List<RetailerCategory>> GetAllRetailerCategories(DateTime lastUpdatedTimestamp);
    }
}
