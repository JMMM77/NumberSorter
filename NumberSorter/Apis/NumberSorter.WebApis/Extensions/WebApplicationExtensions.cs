using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Database;
using NumberSorter.WebApis.Apis;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApis(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseOutputCache();

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NumberSorterDBContext>();

        db.Database.EnsureCreated();
        db.Database.Migrate();

        app.AddSortedResultsApis();

        return app;
    }
}
