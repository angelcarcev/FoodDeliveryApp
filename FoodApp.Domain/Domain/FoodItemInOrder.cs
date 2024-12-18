

namespace FoodApp.Domain.Domain
{
    public class FoodItemInOrder : BaseEntity
    {
        public Guid FoodId { get; set; }
        public FoodItem? FoodItem { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
