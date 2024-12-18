

namespace FoodApp.Domain.Domain
{
    public class FoodItemInShoppingCart : BaseEntity
    {
        public Guid FoodItemId { get; set; }
        public FoodItem? FoodItem { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
