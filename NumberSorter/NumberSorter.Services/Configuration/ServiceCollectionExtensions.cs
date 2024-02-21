using Microsoft.Extensions.DependencyInjection;
using NumberSorter.Services.Mappings;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNumberSorterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(SortedNumbersProfile));
            services.AddScoped<ISortedNumbersService, SortedNumbersService>();

            return services;
        }
    }
}
