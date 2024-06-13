using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseTransportCompany
{
    public class ModelTransport
    {
        public ModelTransport()
        {
            TransportVehicles = new HashSet<TransportVehicle>();
        }

        [Key]
        public int modelId { get; set; }

        [MaxLength(15)]
        public string title { get; set; }

        [ForeignKey("Stamps")]
        public int stampId { get; set; }

        public virtual ICollection<TransportVehicle> TransportVehicles { get; set; }
        public virtual StampTransport Stamps { get; set; }

        public override string ToString()
        {
            return $"Модель: Id — {modelId}, название — {title}, Id марки — {stampId}";
        }
    }
}