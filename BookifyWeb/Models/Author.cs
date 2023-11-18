using System.ComponentModel.DataAnnotations;

namespace BookifyWeb.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

    }
}
