using FoodApp.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(Guid? id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        public IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<FoodItem> GetFoodItemsByRestaurantId(Guid resId);
    }
}
