using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseTransportCompany
{
    public class DriverCategory
    {
        [Key, ForeignKey("Drivers")]
        public int driverId { get; set; }

        [Key, ForeignKey("Categories")]
        public int categoryId { get; set; }

        public virtual Driver Drivers { get; set; }
        public virtual Category Categories { get; set; }

        public override string ToString()
        {
            return $"Водитель_категория: Id водителя — {driverId}, Id категории — {categoryId}";
        }
    }
}