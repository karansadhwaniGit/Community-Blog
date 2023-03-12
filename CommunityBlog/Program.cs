using FluentMigrator.Runner;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using CommunityBlog;
using FluentValidation.AspNetCore;
using CommunityBlog.Models.User;
using FluentValidation;
using CommunityBlog.Validators;
using Microsoft.EntityFrameworkCore;
using CommunityBlog.Services;
using CommunityBlog.Factory;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();


builder.Services.AddTransient<IValidator<UserModel>,UserValidator>();

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(config =>
        config.AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(Assembly.GetExecutingAssembly()).For.All())
        .AddLogging(config => config.AddFluentMigratorConsole());

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthFactory, AuthFactory>();

builder.Services.AddScoped<ITokenHandlerService, TokenHandlerService>();
builder.Services.AddScoped<ITokenHandlerFactory,TokenHandlerFactory>();

builder.Services.AddScoped<IGroupsService, GroupsService>();
builder.Services.AddScoped<IGroupsFactory, GroupsFactory>();

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostFactory,PostFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
using var scope = app.Services.CreateScope();
var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();

migrator.MigrateUp(130920221510);

app.Run();

