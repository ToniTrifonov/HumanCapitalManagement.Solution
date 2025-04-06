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
using HumanCapitalManagement.Handlers.Commands.Employees.Add;
using HumanCapitalManagement.Handlers.Commands.Employees.Delete;
using HumanCapitalManagement.Handlers.Commands.Employees.Edit;
using HumanCapitalManagement.Handlers.Commands.Projects;
using HumanCapitalManagement.Handlers.Queries.Employees;
using HumanCapitalManagement.Handlers.Queries.Projects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

builder.Services.AddScoped<CreateAccountCommandHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>>(sp =>
{
    var mainHandler = sp.GetRequiredService<CreateAccountCommandHandler>();
    return new CreateAccountErrorHandler(mainHandler);
});

builder.Services.AddScoped<IAsyncQueryHandler<ProjectsByUserIdQuery, ProjectsByUserIdResult>, ProjectsByUserIdQueryHandler>();

builder.Services.AddScoped<CreateProjectCommandHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>>(sp =>
{
    var mainHandler = sp.GetRequiredService<CreateProjectCommandHandler>();
    return new CreateProjectErrorHandler(mainHandler);
});

builder.Services.AddScoped<IAsyncQueryHandler<EmployeesByProjectIdQuery, EmployeesByProjectIdResult>, EmployeesByProjectIdQueryHandler>();

builder.Services.AddScoped<AddEmployeeCommandHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>>(sp =>
{
    var mainHandler = sp.GetRequiredService<AddEmployeeCommandHandler>();
    return new AddEmployeeErrorHandler(mainHandler);
});

builder.Services.AddScoped<IAsyncQueryHandler<EmployeeByIdQuery, EmployeeByIdResult>, EmployeeByIdQueryHandler>();

builder.Services.AddScoped<EditEmployeeCommandHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>>(sp =>
{
    var mainHandler = sp.GetRequiredService<EditEmployeeCommandHandler>();
    return new EditEmployeeErrorHandler(mainHandler);
});

builder.Services.AddScoped<DeleteEmployeeCommandHandler>();
builder.Services.AddScoped<IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>>(sp =>
{
    var mainHandler = sp.GetRequiredService<DeleteEmployeeCommandHandler>();
    return new DeleteEmployeeErrorHandler(mainHandler);
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US") };
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

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

app.UseRequestLocalization();

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
