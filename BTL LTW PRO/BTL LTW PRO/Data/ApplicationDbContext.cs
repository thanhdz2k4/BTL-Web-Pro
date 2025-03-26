using BTL_LTW_PRO.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW_PRO.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(); // Đảm bảo email không trùng lặp

            base.OnModelCreating(modelBuilder);
        }
    }
}
