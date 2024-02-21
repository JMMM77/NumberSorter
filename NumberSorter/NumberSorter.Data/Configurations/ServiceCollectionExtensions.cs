using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Repositories;

namespace NumberSorter.Data.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNumberSorterData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NumberSorterDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NumberSorterDatabase")));

            services.AddScoped<ISortedNumbersRespository, SortedNumbersRespository>();

            return services;
        }
    }
}
