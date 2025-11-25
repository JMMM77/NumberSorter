using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Options;
using NumberSorter.Services.Services;
using NumberSorter.Shared.Constants;

namespace NumberSorter.Services.Extensions;

public static class IHostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddServiceDependencies(this IHostApplicationBuilder builder)
    {
        builder.ConfigureDistributedCaching();

        builder.Services.AddScoped<ISortedResultsService, SortedResultsService>();

        return builder;
    }

    private static IHostApplicationBuilder ConfigureDistributedCaching(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<DistributedCachingOptions>(
            builder.Configuration.GetSection(DistributedCachingOptions.DistributedCachingSettings));

        var enabledOutputCaching = builder.Configuration.GetValue<bool>($"{DistributedCachingOptions.DistributedCachingSettings}:{nameof(DistributedCachingOptions.Enabled)}");

        if (enabledOutputCaching)
        {
            builder.AddRedisDistributedCache(AspireResourceNameConstants.CacheName);
            builder.Services.AddScoped<ISortedResultsCachingService, SortedResultsCachingService>();
        }
        else
        {
            builder.Services.AddScoped<ISortedResultsCachingService, NoOpSortedResultsCachingService>();
        }

        return builder;
    }
}
