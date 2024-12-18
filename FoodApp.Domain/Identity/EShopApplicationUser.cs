using FoodApp.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace FoodApp.Domain.Identity
{
    public class EShopApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Address { get; set; }
        public ShoppingCart? UserCart { get; set; }
    }
}
