using NumberSorter.Shared;
using NumberSorter.Shared.Constants;
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
        builder.Services.AddHttpClient<ISortedResultsApiClient, SortedResultsApiClient>(
            static client => client.BaseAddress = new(GetWebApiBaseAddress()));

        builder.Services.AddHttpClient<ILlmChatApiClient, LlmChatApiClient>(
            static client => client.BaseAddress = new(GetWebApiBaseAddress()));

        builder.Services.AddScoped<ISortedResultsService, SortedResultsService>();
        builder.Services.AddScoped<ILlmChatService, LlmChatService>();

        return builder;
    }

    private static string GetWebApiBaseAddress()
        => $"https+http://{AspireResourceNameConstants.WebApiProjectName}";
}
