using NumberSorter.AppHost.Constants;
using NumberSorter.WebUI.Clients;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Services;

namespace NumberSorter.WebUI.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // https://aspire.dev/fundamentals/service-discovery/#named-endpoints-using-configuration
        builder.Services.AddHttpClient<ISortedNumbersApiClient, SortedNumbersApiClient>(
            static client => client.BaseAddress = new($"https+http://{AspireResourceNameConstants.WebApiProjectName}"));

        builder.Services.AddScoped<ISortedNumbersService, SortedNumbersService>();

        return builder;
    }
}
