using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace CarRental.Data
{
    public class ApplicationDbContext : DbContext

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)

        {}

        public DbSet<Client>? Clients { get; set; }
        public DbSet<Client>? Employees { get; set; }
        public DbSet<Client>? Administrators { get; set; }
        public DbSet<Car>? Cars { get; set; }
        public DbSet<Rental>? Rentals { get; set; }
        public DbSet<CarRental.Models.Administrator> Administrator { get; set; } = default!;
        public DbSet<CarRental.Models.Employee> Employee { get; set; } = default!;
        public DbSet<CarRental.Models.Car> Car { get; set; } = default!;
    }
}
