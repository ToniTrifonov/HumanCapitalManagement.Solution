using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Results.Accounts;
using HumanCapitalManagement.Handlers.Commands.Accounts;

namespace HumanCapitalManagement.Web.Extensions
{
    public static class AccountsServicesExtensions
    {
        public static IServiceCollection RegisterAccountServices(this IServiceCollection services)
        {
            services.AddScoped<CreateAccountCommandHandler>();
            services.AddScoped<IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>>(sp =>
            {
                var mainHandler = sp.GetRequiredService<CreateAccountCommandHandler>();
                return new CreateAccountErrorHandler(mainHandler);
            });

            return services;
        }
    }
}
