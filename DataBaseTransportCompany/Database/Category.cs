using System.ComponentModel.DataAnnotations;

namespace DataBaseTransportCompany
{
    public class Category
    {
        public Category()
        {
            DriverCategories = new HashSet<DriverCategory>();
        }

        [Key]
        public int categoryId { get; set; }

        [MaxLength(10)]
        public string title { get; set; }

        // Навигационные свойства
        public virtual ICollection<DriverCategory> DriverCategories { get; set; }

        public override string ToString()
        {
            return $"Категория: Id — {categoryId}, название — {title}";
        }
    }
}