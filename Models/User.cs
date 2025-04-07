namespace book_diplom2.Models
{
    public class User
    {
        public int UserId { get; set; }              // Соответствует user_id
        public string Username { get; set; }         // Соответствует username
        public string UserPassword { get; set; }     // Соответствует user_password
        public DateTime RegistrDay { get; set; }     // Соответствует registr_day
    }
}
