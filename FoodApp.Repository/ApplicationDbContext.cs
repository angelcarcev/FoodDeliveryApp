using FoodApp.Domain.Domain;
using FoodApp.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodApp.Repository
{
    public class ApplicationDbContext : IdentityDbContext <EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<FoodItem> FoodItems { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public virtual DbSet<Restaurant> Restaurants { get; set;}
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<FoodItemInShoppingCart> FoodItemInShoppingCarts { get; set; }
        public virtual DbSet<FoodItemInOrder> FoodItemInOrders { get; set; }
    }
}

