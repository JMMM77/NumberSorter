using NumberSorter.AppHost.Constants;
using NumberSorter.Data;
using NumberSorter.Data.Configurations;
using NumberSorter.Services.Configuration;

namespace NumberSorter.WebApis.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddServiceDefaults();

        builder.Services.AddOpenApi();

        builder.AddSqlServerDbContext<NumberSorterDBContext>(AspireResourceNameConstants.SqlDatabaseName);

        builder.Services.AddNumberSorterData(builder.Configuration);

        builder.Services.AddNumberSorterServices();

        return builder;
    }
}
