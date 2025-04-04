using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Admin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().Property(x => x.Salary).HasPrecision(18, 2);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSeeding((context, _) =>
                {
                    DatabaseSeeder.Seed(context);
                })
                .UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    await DatabaseSeeder.SeedAsync(context);
                });
        }
    }
}
