using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Extensions;

public static class IHostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddServiceDependencies(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISortedResultsService, SortedResultsService>();

        return builder;
    }
}
