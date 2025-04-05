﻿using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Handlers.Commands.Employees
{
    public class DeleteEmployeeCommandHandler : IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        private readonly ApplicationDbContext context;

        public DeleteEmployeeCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<DeleteEmployeeResult> HandleAsync(DeleteEmployeeCommand command)
        {
            var employee = await this.context.Set<Employee>().FirstOrDefaultAsync(employee => employee.Id == command.Id);
            if (employee == null)
            {
                return new DeleteEmployeeResult("An employee with that id does not exist.");
            }

            employee.IsDeleted = true;

            await this.context.SaveChangesAsync();
            return new DeleteEmployeeResult();
        }
    }
}
