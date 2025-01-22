﻿using FoodApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Interface
{
    public interface IPartnerService
    {
        public IEnumerable<Restaurant> GetRestaurants();
    }
}
