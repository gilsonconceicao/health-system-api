using HealthSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Data.Contexts
{
    public class PostgresDbContext : DbContext
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; }
    }
}