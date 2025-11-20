using NumberSorter.Services.Configuration;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddOpenApi();

        builder.Services.AddNumberSorterServices();

        return builder;
    }
}
