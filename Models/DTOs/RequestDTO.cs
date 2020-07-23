namespace Models.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public bool? IsAccepted { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
    }
}
