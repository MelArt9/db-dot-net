using System.ComponentModel.DataAnnotations;

namespace DataBaseTransportCompany
{
    public class TypeTransport
    {
        public TypeTransport()
        {
            TransportVehicles = new HashSet<TransportVehicle>();
        }

        [Key]
        public int typeId { get; set; }

        [MaxLength(30)]
        public string title { get; set; }

        public virtual ICollection<TransportVehicle> TransportVehicles { get; set; }

        public override string ToString()
        {
            return $"Тип: Id — {typeId}, название — {title}";
        }
    }
}