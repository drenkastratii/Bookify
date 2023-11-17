using System.ComponentModel.DataAnnotations;

namespace BookifyWeb.Models
{
    public class Category
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }    
    }
}
