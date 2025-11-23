using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NumberSorter.AppHost.Constants;
using NumberSorter.Data.Database;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Repositories;

namespace NumberSorter.Data.Extensions;

public static class IHostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddDataDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<NumberSorterDBContext>(AspireResourceNameConstants.SqlDatabaseName);

        builder.Services.AddScoped<ISortedNumbersRespository, SortedNumbersRespository>();

        return builder;
    }
}
