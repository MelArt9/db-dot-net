using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseTransportCompany
{
    public class TransportVehicle
    {
        public TransportVehicle()
        {
            TransportVehicleDrivers = new HashSet<TransportVehicleDriver>();
        }

        [Key]
        public int transportVehicleId { get; set; }

        [MaxLength(6)]
        public string number { get; set; }

        [CheckConstraint("codeRegion > 0")]
        public int codeRegion { get; set; }

        [CheckConstraint("dateLastInspection IS NULL OR (dateLastInspection >= '1930-01-01' AND dateLastInspection <= CURRENT_DATE)")]
        public DateOnly? dateLastInspection { get; set; }

        [MaxLength(14)]
        public string engineNumber { get; set; }

        [CheckConstraint("numberSeats > 0")]
        public int numberSeats { get; set; }

        [CheckConstraint("numberStandingPlaces IS NULL OR numberStandingPlaces > 0")]
        public int? numberStandingPlaces { get; set; }

        [CheckConstraint("maxSpeed > 0 AND maxSpeed < 1228")]
        public int maxSpeed { get; set; }

        [CheckConstraint("releaseDate <= CURRENT_DATE")]
        public DateOnly releaseDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Models")]
        public int modelId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Types")]
        public int typeId { get; set; }

        public virtual ICollection<TransportVehicleDriver> TransportVehicleDrivers { get; set; }
        public virtual TypeTransport Types { get; set; }
        public virtual ModelTransport Models { get; set; }

        public override string ToString()
        {
            return $"Транспорт: Id — {transportVehicleId}, номер — {number}, код региона — {codeRegion}, " +
                   $"дата последнего ТО — {dateLastInspection}, номер двигателя — {engineNumber}, " +
                   $"кол-во посадочных мест — {numberSeats}, кол-во стоячих мест — {numberStandingPlaces}, " +
                   $"максимальная скорость — {maxSpeed}, дата выпуска — {releaseDate}, Id модели — {modelId}, Id типа — {typeId}";
        }
    }
}