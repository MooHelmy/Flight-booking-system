using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);   // لازم أول سطر، بتجهز جداول Identity

        modelBuilder.Entity<Booking>()
              .HasIndex(b => b.BookingReference)
              .IsUnique();
        //     modelBuilder.Entity<Product>(entity =>
        // {
        //     entity.Property(p => p.Name)
        //         .IsRequired()
        //         .HasMaxLength(150);

        //     entity.Property(p => p.Description)
        //         .HasMaxLength(1000);

        //     entity.Property(p => p.ImageUrl)
        //         .HasMaxLength(500);

        //     entity.Property(p => p.Price)
        //         .HasPrecision(18, 2);
        // });

        //     // Category Configuration
        //     modelBuilder.Entity<Category>(entity =>
        //     {
        //         entity.Property(c => c.Name)
        //             .IsRequired()
        //             .HasMaxLength(100);
        //     });
    }

    public DbSet<Flight> Flights { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<FareClass> FarClasses { get; set; }
    public DbSet<QueueTicket> QueueTickets { get; set; }
    public DbSet<SeatHold> SeatHolds { get; set; }

}
