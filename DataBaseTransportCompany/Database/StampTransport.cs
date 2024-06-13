using System.ComponentModel.DataAnnotations;

namespace DataBaseTransportCompany
{
    public class StampTransport
    {
        public StampTransport()
        {
            Models = new HashSet<ModelTransport>();
        }

        [Key]
        public int stampId { get; set; }

        [MaxLength(15)]
        public string title { get; set; }

        public virtual ICollection<ModelTransport> Models { get; set; }

        public override string ToString()
        {
            return $"Марка: Id — {stampId}, название — {title}";
        }
    }
}