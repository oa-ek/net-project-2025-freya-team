using System.ComponentModel.DataAnnotations;

namespace FlowerWEB.Models
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Forming> Formings { get; set; } = new List<Forming>();
        public ICollection<FlowersForOrder> FlowersForOrders { get; set; } = new List<FlowersForOrder>();
    }
}
