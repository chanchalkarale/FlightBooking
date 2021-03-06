using FlightBookingService.Airline.Models; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.User.DataContext
{
    public class AirlineServiceContext:DbContext
    {

        public AirlineServiceContext(DbContextOptions<AirlineServiceContext> options):base(options)
        {

        }

        public DbSet<AirlineDetails> AirlineDetails { get; set; }
        public DbSet<AirlineFlightDetails> AirlineFlightDetails { get; set; }
        public DbSet<FlightBookingDetails> FlightBookingDetails { get; set; }
        public DbSet<UserBookingDetails> UserBookingDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        
        public DbSet<AirlineFlightDetailsRawQueryModel> airlineFlightDetailsRawQueryModels { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirlineFlightDetailsRawQueryModel>()
                .HasNoKey();
        }

    }
}
