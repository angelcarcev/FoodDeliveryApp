using FoodApp.Domain.Identity;

namespace FoodApp.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public IEnumerable<FoodItemInOrder>? FoodItemsInOrder { get; set; }

    }
}
