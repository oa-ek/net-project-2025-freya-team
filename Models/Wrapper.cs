using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FREYA_WEB.Models
{
    public class Wrapper
    {
        [Key]
        public int WrapperId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Bouquet> Bouquets { get; set; } = new List<Bouquet>();
    }
}

