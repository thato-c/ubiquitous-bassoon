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
        }
    }
}
