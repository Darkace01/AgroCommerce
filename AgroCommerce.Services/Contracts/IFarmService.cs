﻿using AgroCommerce.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCommerce.Services.Contracts
{
    public interface IFarmService
    {
        Task SetupFarm(Farm farm, ApplicationUser farmOwner);
        IEnumerable<Farm> GetAll();
        Farm GetByID(long Id);
        Task UpdateFarm(Farm farm, ApplicationUser farmOwner);
        int GetAllFarmNumber(string id);
        Farm GetFarmByUserID(string userId);
    }
}
