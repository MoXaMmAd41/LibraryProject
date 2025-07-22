using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library.Dto
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^(?!\\d+$)[a-zA-Z0-9]+$", ErrorMessage = "The name cannot be just numberes.")]
        public string Name { get; set; }
        [Required]
        [Range(0,1000)]
        public decimal Price { get; set; }
        public int ShelfId { get; set; }
    }
}
