using Microsoft.EntityFrameworkCore;
using sullied_data.Models;

namespace sullied_data
{
    public class SulliedDbContext : DbContext
    {
        public SulliedDbContext(DbContextOptions<SulliedDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<EventUserEntity> EventUsers { get; set; }
        public DbSet<EventLocationEntity> EventLocations { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<PollEntity> Polls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventUserEntity>()
                .HasKey(eu => new { eu.EventId, eu.UserId });

            modelBuilder.Entity<EventUserEntity>()
                .HasOne(bc => bc.Event)
                .WithMany(b => b.EventUsers)
                .HasForeignKey(bc => bc.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventUserEntity>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.EventUsers)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventLocationEntity>()
                .HasKey(eu => new { eu.EventId, eu.LocationId });

            modelBuilder.Entity<EventLocationEntity>()
                .HasOne(bc => bc.Event)
                .WithMany(b => b.EventLocations)
                .HasForeignKey(bc => bc.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EventLocationEntity>()
                .HasOne(bc => bc.Location)
                .WithMany(c => c.EventLocations)
                .HasForeignKey(bc => bc.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
