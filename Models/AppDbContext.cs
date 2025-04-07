using Microsoft.EntityFrameworkCore;

namespace book_diplom2.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } // Таблица пользователей

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
        }
    }
}
