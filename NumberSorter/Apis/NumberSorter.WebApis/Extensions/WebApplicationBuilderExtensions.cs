using Microsoft.Extensions.Options;
using NumberSorter.Data.Extensions;
using NumberSorter.Services.Extensions;
using NumberSorter.Shared;
using NumberSorter.WebApis.Options;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddOpenApi();

        builder
            .ConfigureOutputCaching()
            .AddDataDependencies()
            .AddServiceDependencies();

        return builder;
    }

    private static WebApplicationBuilder ConfigureOutputCaching(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<OutputCachingOptions>(
            builder.Configuration.GetSection(OutputCachingOptions.OutputCachingSettings));

        var options = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<OutputCachingOptions>>().Value;

        if (options.Enabled)
        {
            builder.Services.AddOutputCache(options =>
            {
                options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromSeconds(10)));
            });
        }

        return builder;
    }
}
