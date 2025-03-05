using System.ComponentModel.DataAnnotations;

namespace FlowerWEB.Models
{
    public class FlowersForOrder
    {
        [Key]
        public int FlowOId { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int FlowerId { get; set; }
        public Flower Flower { get; set; }

        public int Count { get; set; }
    }

}
