using Microsoft.EntityFrameworkCore;
using PrimeiraAPI.Domain.Model;
using PrimeiraAPI.Domain.Model.CompanyAggregate;

namespace PrimeiraAPI.Infraestrutura.Repositories
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=employee_sample;User Id=postgres;Password=1234");


        }
    }
}
