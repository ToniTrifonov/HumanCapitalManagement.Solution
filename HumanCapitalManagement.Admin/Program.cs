using HumanCapitalManagement.Admin.Data;
using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Contracts.Results.Accounts;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Handlers.Commands.Accounts;
using HumanCapitalManagement.Handlers.Commands.Employees;
using HumanCapitalManagement.Handlers.Commands.Projects;
using HumanCapitalManagement.Handlers.Queries.Employees;
using HumanCapitalManagement.Handlers.Queries.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>, CreateAccountCommandHandler>();
builder.Services.AddScoped<IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult>, ProjectsByUserIdQueryHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>, CreateProjectCommandHandler>();
builder.Services.AddScoped<IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult>, EmployeesByProjectIdQueryHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>, AddEmployeeCommandHandler>();
builder.Services.AddScoped<IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult>, EmployeeByIdQueryHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>, EditEmployeeCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
app.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
