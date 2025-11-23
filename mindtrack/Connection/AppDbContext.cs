using Microsoft.EntityFrameworkCore;
using mindtrack.Model;

namespace mindtrack.Connection
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<CheckinHumor> Checkins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheckinHumor>()
            .Property(c => c.StatusHumor)
            .HasConversion<string>(); 
            base.OnModelCreating(modelBuilder);
        }
    }
}
