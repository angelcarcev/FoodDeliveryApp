using FoodApp.Domain.Identity;

namespace FoodApp.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public virtual ICollection<FoodItemInShoppingCart>? FoodItemInShoppingCarts { get; set; }

    }
}
