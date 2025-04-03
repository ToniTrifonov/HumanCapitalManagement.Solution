using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Queries.Roles;
using HumanCapitalManagement.Contracts.Results.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Queries.Roles
{
    public class AllRolesQueryHandler : IAsyncQueryHandler<AllRolesQuery, AllRolesResult>
    {
        private readonly ApplicationDbContext context;

        public AllRolesQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AllRolesResult> HandleAsync(AllRolesQuery query)
        {
            var allRoles = await this.context.Set<IdentityRole>()
                .Select(role => new RoleItem(role.Id, role.Name))
                .ToListAsync();

            return new AllRolesResult(allRoles);
        }
    }
}
