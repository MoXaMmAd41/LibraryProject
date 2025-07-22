using System.ComponentModel.DataAnnotations;
using Library.Models;

namespace Library.Dto
{
    public class ShelfDto
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(?!\d+$)[a-zA-Z0-9]+$", ErrorMessage = "The name cannot be just numbers.")]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
