namespace Models.DTOs
{
    public class InsertBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int CopiesCount { get; set; }
        public string CoverPhotoId { get; set; }
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }
    }
}
