using FoodApp.Domain.Domain;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.FoodItemsInOrder)
                .Include(z => z.Owner)
                .Include("FoodItemsInOrder.FoodItem")
                .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.FoodItemsInOrder)
                .Include(z => z.Owner)
                .Include("FoodItemsInOrder.FoodItem")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
