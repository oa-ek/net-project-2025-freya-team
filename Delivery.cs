using System.ComponentModel.DataAnnotations;

namespace FlowerWEB.Models
{
    public class Delivery
    {
        [Key]
        public int DeliveryId { get; set; }

        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; }


        public string Region { get; set; }
        public string District { get; set; }
        public string CityOrVillage { get; set; }
        public string Street { get; set; }
        public string Address { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
