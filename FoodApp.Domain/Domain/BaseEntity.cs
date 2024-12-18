using System.ComponentModel.DataAnnotations;

namespace FoodApp.Domain.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
