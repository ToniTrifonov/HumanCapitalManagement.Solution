using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Handlers.Commands.Employees.Add;
using HumanCapitalManagement.Handlers.Commands.Employees.Delete;
using HumanCapitalManagement.Handlers.Commands.Employees.Edit;
using HumanCapitalManagement.Handlers.Queries.Employees;

namespace HumanCapitalManagement.Web.Extensions
{
    public static class EmployeesServicesExtensions
    {
        public static IServiceCollection RegisterEmployeeServices(this IServiceCollection services)
        {
            services.AddScoped<AddEmployeeCommandHandler>();
            services.AddScoped<IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>>(sp =>
            {
                var mainHandler = sp.GetRequiredService<AddEmployeeCommandHandler>();
                return new AddEmployeeErrorHandler(mainHandler);
            });

            services.AddScoped<EditEmployeeCommandHandler>();
            services.AddScoped<IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>>(sp =>
            {
                var mainHandler = sp.GetRequiredService<EditEmployeeCommandHandler>();
                return new EditEmployeeErrorHandler(mainHandler);
            });

            services.AddScoped<DeleteEmployeeCommandHandler>();
            services.AddScoped<IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>>(sp =>
            {
                var mainHandler = sp.GetRequiredService<DeleteEmployeeCommandHandler>();
                return new DeleteEmployeeErrorHandler(mainHandler);
            });

            services.AddScoped<IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult>, EmployeesByProjectIdQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult>, EmployeeByIdQueryHandler>();

            return services;
        }
    }
}
