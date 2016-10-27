﻿using UnityCard.Models;
using System;
using System.Collections.Generic;
namespace UnityCard.BusinessLayer.Repositories.Interfaces
{
    public interface IRetailerLocationRepository : IGenericRepository<RetailerLocation>
    {
        List<RetailerLocation> GetRetailerLocations(int retailerId);
        RetailerLocation GetRetailerLocation(int locationId);
    }
}
