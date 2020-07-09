namespace Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public int CopiesCount { get; set; }
        public string CoverPhotoId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
