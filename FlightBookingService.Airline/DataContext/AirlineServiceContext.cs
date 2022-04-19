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

        public DbSet<AirlineFlightDetails> AirlineFlightDetails { get; set; }
        public DbSet<FlightBookingDetails> FlightBookingDetails { get; set; }
        public DbSet<UserBookingDetails> UserBookingDetails { get; set; }
    }
}
