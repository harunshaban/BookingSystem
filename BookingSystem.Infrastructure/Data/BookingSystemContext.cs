using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.Data
{
    public class BookingSystemContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        
        public BookingSystemContext(DbContextOptions<BookingSystemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>();
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Resource)
                .WithMany()
                .HasForeignKey(b => b.ResourceId);
        }
    }
}
