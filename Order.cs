using System.ComponentModel.DataAnnotations;

namespace FlowerWEB.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }

        public int DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<FlowersForOrder> FlowersForOrders { get; set; } = new List<FlowersForOrder>();
        public ICollection<BouquetsForOrder> BouquetsForOrders { get; set; } = new List<BouquetsForOrder>();
    }
}
