using FlightBookingService.User.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBookingService.User.DataContext
{
    public class UserServiceContext:DbContext
    {

        public UserServiceContext(DbContextOptions<UserServiceContext> options):base(options)
        {

        }

        public DbSet<UserRegistration> userRegistrations { get; set; }
    }
}
