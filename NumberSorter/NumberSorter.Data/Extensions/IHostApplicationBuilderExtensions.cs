using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NumberSorter.Data.Database;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Repositories;
using NumberSorter.Shared.Constants;

namespace NumberSorter.Data.Extensions;

public static class IHostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddDataDependencies(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<NumberSorterDBContext>(AspireResourceNameConstants.SqlDatabaseName);

        builder.Services.AddScoped<ISortedResultsRespository, SortedResultsRespository>();

        return builder;
    }
}
