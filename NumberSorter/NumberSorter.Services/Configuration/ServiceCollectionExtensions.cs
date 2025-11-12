using Microsoft.Extensions.DependencyInjection;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mappings;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Configuration;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection with services required for number sorting operations.
    /// </summary>
    /// <param name="services">The collection of services to extend.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddNumberSorterServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<SortedNumbersProfile>();
        });
        services.AddScoped<ISortedNumbersService, SortedNumbersService>();

        return services;
    }
}
