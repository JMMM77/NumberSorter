using NumberSorter.Data.Extensions;
using NumberSorter.Services.Extensions;
using NumberSorter.Shared;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddOpenApi();

        builder.Services.AddOutputCache(options =>
            {
                options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(10)));
            });

        builder
            .AddDataDependencies()
            .AddServiceDependencies();

        return builder;
    }
}
