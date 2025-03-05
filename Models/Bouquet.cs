using System.ComponentModel.DataAnnotations;

namespace FREYA_WEB.Models
{
    public class Bouquet
    {
        [Key]
        public int BouquetId { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Height { get; set; }

        public int? PostcardId { get; set; }
        public Postcard Postcard { get; set; }

        public int? WrapperId { get; set; }
        public Wrapper Wrapper { get; set; }

        public string PhotoUrl { get; set; }

        public ICollection<BouquetsForOrder> BouquetsForOrders { get; set; } = new List<BouquetsForOrder>();
        public ICollection<Forming> Formings { get; set; } = new List<Forming>();
    }
}
