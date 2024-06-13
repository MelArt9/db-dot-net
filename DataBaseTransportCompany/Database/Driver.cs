using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseTransportCompany
{
    public class Driver
    {
        public Driver()
        {
            TransportVehicleDrivers = new HashSet<TransportVehicleDriver>();
            DriverCategories = new HashSet<DriverCategory>();
        }

        [Key]
        public int driverId { get; set; }

        [MaxLength(20)]
        public string surname { get; set; }

        [MaxLength(20)]
        public string name { get; set; }

        [MaxLength(20)]
        public string? patronymic { get; set; }

        [CheckConstraint("dateOfBirth <= (CURRENT_DATE - INTERVAL '18 years')")]
        public DateOnly dateOfBirth { get; set; }

        [MaxLength(20)]
        public string phone { get; set; }

        [MaxLength(12)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string driverLicenseNumber { get; set; }

        [CheckConstraint("validityPeriodRights >= CURRENT_DATE")]
        public DateOnly validityPeriodRights { get; set; }

        [CheckConstraint("experience <= CURRENT_DATE")]
        public DateOnly experience { get; set; }

        [MaxLength(100)]
        public string address { get; set; }

        public virtual ICollection<TransportVehicleDriver> TransportVehicleDrivers { get; set; }
        public virtual ICollection<DriverCategory> DriverCategories { get; set; }

        public override string ToString()
        {
            return $"Водитель: Id — {driverId}, фамилия — {surname}, имя — {name}, отчество — {patronymic}, дата рождения — {dateOfBirth}, " +
                   $"телефон — {phone}, номер водительского удостоверения — {driverLicenseNumber}, " +
                   $"срок действия прав — {validityPeriodRights}, стаж — {experience}, адресс проживания — {address}";
        }
    }
}