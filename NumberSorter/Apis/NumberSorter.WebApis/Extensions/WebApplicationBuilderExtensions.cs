using NumberSorter.Data.Extensions;
using NumberSorter.Services.Extensions;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddOpenApi();

        builder.AddDataDependencies();

        builder.AddServiceDependencies();

        return builder;
    }
}
