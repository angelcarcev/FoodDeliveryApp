using FoodApp.Domain.Domain;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        public RestaurantService(IRepository<Restaurant> restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void CreateNewRestaurant(Restaurant r)
        {
            _restaurantRepository.Insert(r);
        }

        public void DeleteRestaurant(Guid id)
        {
            var res = _restaurantRepository.Get(id);
            _restaurantRepository.Delete(res);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public Restaurant GetDetailsForRestaurant(Guid? id)
        {
            var res = _restaurantRepository.Get(id);
            return res;
        }

        public void UpdateExistingProduct(Restaurant r)
        {
            _restaurantRepository.Update(r);
        }
    }
}
