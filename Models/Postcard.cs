using System.ComponentModel.DataAnnotations;

namespace FREYA_WEB.Models
{
    public class Postcard
    {
        [Key]
        public int PostcardId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Bouquet> Bouquets { get; set; } = new List<Bouquet>();
    }

}
