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
    public class FoodItemService : IFoodItemService
    {
        private readonly IRepository<FoodItem> _foodItemRepository;


        public FoodItemService(IRepository<FoodItem> foodItemRepository)
        {
            _foodItemRepository = foodItemRepository;
        }

        public void CreateNewFoodItem(FoodItem fi)
        {
            _foodItemRepository.Insert(fi);
        }

        public void DeleteFoodItem(Guid id)
        {
            var foodItem = _foodItemRepository.Get(id);
            _foodItemRepository.Delete(foodItem);
        }

        public List<FoodItem> GetAllFoodItems()
        {

            return _foodItemRepository.GetAllIncluding(fi => fi.Restaurant).ToList();
        }

        public FoodItem GetDetailsForFoodItem(Guid? id)
        {
            var foodItem = _foodItemRepository.Get(id);
            return foodItem;
        }

        public IEnumerable<FoodItem> getFoodItems(Guid resId)
        {
            return _foodItemRepository.GetFoodItemsByRestaurantId(resId);
        }

        public void UpdateExistingFoodItem(FoodItem fi)
        {
            _foodItemRepository.Update(fi);
        }
    }
}
