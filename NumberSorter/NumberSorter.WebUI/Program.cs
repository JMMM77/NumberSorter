using Microsoft.EntityFrameworkCore;
using NumberSorter.Data;
using NumberSorter.Data.Configurations;
using NumberSorter.Services.Configuration;
using NumberSorter.Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.AddSqlServerDbContext<NumberSorterDBContext>(NumberSorterAppConstants.AspireSqlDatabaseName);

builder.Services.AddNumberSorterData(builder.Configuration);

builder.Services.AddNumberSorterServices();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<NumberSorterDBContext>();

db.Database.EnsureCreated();
db.Database.Migrate();

app.MapDefaultEndpoints();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
