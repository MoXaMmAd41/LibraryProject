using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Shelf
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsNotActive { get; set; }
        public ICollection<Book> Books { get; set; }
      
    }
}
