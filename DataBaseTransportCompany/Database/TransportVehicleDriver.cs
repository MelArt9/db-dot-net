using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseTransportCompany
{
    public class TransportVehicleDriver
    {
        [Key, ForeignKey("TransportVehicles")]
        public int transportVehicleId { get; set; }

        [Key, ForeignKey("Drivers")]
        public int driverId { get; set; }

        public virtual TransportVehicle TransportVehicles { get; set; }
        public virtual Driver Drivers { get; set; }

        public override string ToString()
        {
            return $"Транспорт_водитель: Id транспорта — {transportVehicleId}, Id водителя — {driverId}";
        }
    }
}