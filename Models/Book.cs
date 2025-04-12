namespace book_diplom2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Isbn { get; set; }
        public int NumPages { get; set; }
        public string Title { get; set; }
        public int? CountryId { get; set; }
        public int? PubyearId { get; set; }
        public string Note { get; set; }
    }
}
