using FoodApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Service.Interface
{
    public interface IFoodItemService
    {
        List<FoodItem> GetAllFoodItems();
        FoodItem GetDetailsForFoodItem(Guid? id);
        void CreateNewFoodItem(FoodItem fi);
        void UpdateExistingFoodItem(FoodItem fi);
        void DeleteFoodItem(Guid id);

        IEnumerable<FoodItem> getFoodItems(Guid resId);

    }
}
