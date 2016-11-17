using UnityCard.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IRetailerLocationRepository : IGenericRepository<RetailerLocation>
    {
        Task<List<RetailerLocation>> GetRetailerLocations(int retailerId, DateTime lastUpdatedTimestamp);
        Task<RetailerLocation> GetRetailerLocation(int locationId);
    }
}
