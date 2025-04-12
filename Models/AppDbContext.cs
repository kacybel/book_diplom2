using Microsoft.EntityFrameworkCore;

namespace book_diplom2.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PublicationYear> PublicationYears { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настраиваем маппинг на таблицу "user"
            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(u => u.Username).HasColumnName("username");
            modelBuilder.Entity<User>().Property(u => u.UserPassword).HasColumnName("user_password");
            modelBuilder.Entity<User>().Property(u => u.RegistrDay).HasColumnName("registr_day");

            // Book
            modelBuilder.Entity<Book>().ToTable("book");
            modelBuilder.Entity<Book>().HasKey(b => b.BookId);
            modelBuilder.Entity<Book>().Property(b => b.BookId).HasColumnName("book_id");
            modelBuilder.Entity<Book>().Property(b => b.Isbn).HasColumnName("isbn");
            modelBuilder.Entity<Book>().Property(b => b.NumPages).HasColumnName("num_pages");
            modelBuilder.Entity<Book>().Property(b => b.Title).HasColumnName("title");
            modelBuilder.Entity<Book>().Property(b => b.CountryId).HasColumnName("country_id");
            modelBuilder.Entity<Book>().Property(b => b.PubyearId).HasColumnName("pubyear_id");
            modelBuilder.Entity<Book>().Property(b => b.Note).HasColumnName("note");

            // Настраиваем составной ключ для UserBook
            modelBuilder.Entity<UserBook>().HasKey(ub => new { ub.UserId, ub.BookId });
            modelBuilder.Entity<UserBook>().ToTable("userbook");
            modelBuilder.Entity<UserBook>().Property(ub => ub.UserId).HasColumnName("user_id");
            modelBuilder.Entity<UserBook>().Property(ub => ub.BookId).HasColumnName("book_id");
            modelBuilder.Entity<UserBook>().Property(ub => ub.ReadDate).HasColumnName("read_date");
            modelBuilder.Entity<UserBook>().Property(ub => ub.RatingValue).HasColumnName("rating_value");


            // Genre
            modelBuilder.Entity<Genre>().ToTable("genre");

            // BookGenre
            modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.BookId, bg.GenreId });
            modelBuilder.Entity<BookGenre>().ToTable("book_genre");

            // Author
            modelBuilder.Entity<Author>()
                .ToTable("author");

            // BookAuthor
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .ToTable("book_author");

            // Country
            modelBuilder.Entity<Country>()
                .ToTable("country");

            // PublicationYear
            modelBuilder.Entity<PublicationYear>()
                .ToTable("publication_year");
            modelBuilder.Entity<PublicationYear>()
                .HasKey(py => py.PubyearId);
        }
    }
}
