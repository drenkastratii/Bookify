using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 100)]
        public double Price { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
