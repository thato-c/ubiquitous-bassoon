using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentalSite.Models;

namespace RentalSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<WishList> WishList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agent>().ToTable("Agent");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<Favorites>().ToTable("Favorites");
            modelBuilder.Entity<WishList>().ToTable("WishList");

            // Configure the relationship between Agent and Property
            modelBuilder.Entity<Agent>()
                .HasMany(a => a.Properties)
                .WithOne(p => p.Agent)
                .HasForeignKey(p => p.AgentId);

            // Configure Primary Keys
            modelBuilder.Entity<Favorites>().HasKey(f => f.FavoritesId);
            modelBuilder.Entity<WishList>().HasKey(w => w.WishListId);

            // Configure the relationship between Favorites and User
            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .IsRequired();

            modelBuilder.Entity<Favorites>()
                .HasOne(f => f.Property)
                .WithMany()
                .HasForeignKey(f => f.PropertyId)
                .IsRequired();

            // Configure the relationship between WishList and User
            modelBuilder.Entity<WishList>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .IsRequired();

            modelBuilder.Entity<WishList>()
                .HasOne(w => w.Property)
                .WithMany()
                .HasForeignKey(w => w.PropertyId)
                .IsRequired();
        }
    }
}
