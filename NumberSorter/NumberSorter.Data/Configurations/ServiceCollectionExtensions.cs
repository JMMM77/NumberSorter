using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Repositories;

namespace NumberSorter.Data.Configurations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extends the IServiceCollection with services required for number sorting data operations.
    /// </summary>
    /// <param name="services">The collection of services to extend.</param>
    /// <param name="configuration">The configuration containing the connection string for the database.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddNumberSorterData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NumberSorterDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("NumberSorterDatabase")));

        services.AddScoped<ISortedNumbersRespository, SortedNumbersRespository>();

        return services;
    }
}
