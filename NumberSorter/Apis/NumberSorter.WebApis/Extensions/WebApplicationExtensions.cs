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

        app.AddSortedNumbersApis();

        return app;
    }
}
