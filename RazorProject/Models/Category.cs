using System.ComponentModel.DataAnnotations;

namespace RazorProject.Models

{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
