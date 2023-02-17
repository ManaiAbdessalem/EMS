using EMS_Data.EmployeeAggreagate;
using EMS_Domain.EmployeeAggreagate;
using Microsoft.EntityFrameworkCore;

namespace EMS_Data
{
    public class EMSContext : DbContext
    {
        public EMSContext(DbContextOptions<EMSContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new EmployeeEntityTypeConfiguration().Configure(builder.Entity<Employee>());
        }
    }
}