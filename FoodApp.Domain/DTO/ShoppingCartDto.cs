using FoodApp.Domain.Domain;

namespace FoodApp.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<FoodItemInShoppingCart>? FoodItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
