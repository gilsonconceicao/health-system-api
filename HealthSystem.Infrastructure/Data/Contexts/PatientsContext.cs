using HealthSystem.Domain.Entities;
using HealthSystem.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Data.Contexts
{
    public class PatientsContext : DbContext
    {
        public PatientsContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}