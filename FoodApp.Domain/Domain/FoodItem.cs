

namespace FoodApp.Domain.Domain
{
    public class FoodItem : BaseEntity
    {
        public string? FoodItemName { get; set; }
        public string? FoodItemDescription { get; set; }
        public string? FoodItemImage { get; set; }
        public int Price { get; set; }
        public Guid? RestaurantId { get; set; }
        public Restaurant? Restourant { get; set; }
    }
}
