
namespace FoodApp.Domain.DTO
{
    public class AddToCartDto
    {
        public Guid FoodId {  get; set; }
        public string? FoodName { get; set; }
        public int Quantity { get; set; }
    }
}
