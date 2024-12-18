

namespace FoodApp.Domain.Domain
{
    public class Restaurant : BaseEntity
    {
        public string? RestaurantName { get; set; }
        public string? RestaurantDescription { get; set; }
        public string? RestaurantImage { get; set; }
        public string? RestaurantAddress { get; set; }
        public virtual ICollection<FoodItem>? FoodItems { get; set; }
    }
}
