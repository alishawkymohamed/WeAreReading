namespace Models.DTOs
{
    public class CreateRequestDTO
    {
        public int BookId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
