using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IRetailerRepository : IGenericRepository<Retailer>
    {
        Task<List<Retailer>> GetAllRetailers(DateTime lastUpdatedTimestamp);
    }
}
