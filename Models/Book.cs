namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsNotActive { get; set; }
        public int ShelfId { get; set; }
        public Shelf Shelf { get; set; }
       
    }
}
