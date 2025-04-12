namespace book_diplom2.Models
{
    public class UserBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime? ReadDate { get; set; }
        public int? RatingValue { get; set; }
    }
}
