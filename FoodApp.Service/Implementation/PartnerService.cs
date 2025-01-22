using FoodApp.Domain.Domain;
using FoodApp.Service.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Implementation
{
    public class PartnerService : IPartnerService
    {
        private readonly string _connectionString;

        public PartnerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Partner") ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var restaurants = connection.Query("SELECT * FROM dbo.Restaurants");

                List<Restaurant> localRestaurants = new List<Restaurant>();

                foreach (var restaurant in restaurants)
                {
                    var restoran = new Restaurant
                    {
                        Id = restaurant.Id,
                        RestaurantImage =  restaurant.RestaurantImage,
                        RestaurantName =  restaurant.Name,
                        RestaurantAddress = restaurant.Address

                    };
                    localRestaurants.Add(restoran);
                }

                return localRestaurants;
            }
        }

    }
}
