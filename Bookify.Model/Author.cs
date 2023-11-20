    using System.ComponentModel.DataAnnotations;

namespace Bookify.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FullName { get; set; }

    }
}
