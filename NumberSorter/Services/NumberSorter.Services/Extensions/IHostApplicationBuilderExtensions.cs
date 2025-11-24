using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Services;
using NumberSorter.Shared.Constants;

namespace NumberSorter.Services.Extensions;

public static class IHostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddServiceDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddRedisDistributedCache(AspireResourceNameConstants.CacheName);

        builder.Services
            .AddScoped<ISortedResultsCachingService, SortedResultsCachingService>()
            .AddScoped<ISortedResultsService, SortedResultsService>();

        return builder;
    }
}
