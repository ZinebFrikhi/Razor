using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models
{

    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string? Img { get; set; }
        public virtual Category Category { get; set; }

    }
}
