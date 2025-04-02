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
