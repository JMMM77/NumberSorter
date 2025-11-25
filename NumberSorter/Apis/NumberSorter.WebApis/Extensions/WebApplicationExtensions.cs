using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NumberSorter.Data.Database;
using NumberSorter.WebApis.Apis;
using NumberSorter.WebApis.Options;

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

        var enableOutputCaching = app.Services.GetRequiredService<IOptions<OutputCachingOptions>>().Value.Enabled;

        if (enableOutputCaching)
        {
            app.UseOutputCache();
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NumberSorterDBContext>();

        db.Database.EnsureCreated();
        db.Database.Migrate();

        app.AddSortedResultsApis();

        return app;
    }
}
