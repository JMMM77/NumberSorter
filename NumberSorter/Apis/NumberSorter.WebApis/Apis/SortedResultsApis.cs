using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;

namespace NumberSorter.WebApis.Apis;

internal static class SortedResultsApis
{
    public const string SortedResultsApiPath = "sorted-results";

    public static WebApplication AddSortedResultsApis(this WebApplication app)
    {
        var group = app.MapGroup($"/{SortedResultsApiPath}");

        group.MapGet("/", async Task<IResult> (ISortedResultsService sortedResultsService, CancellationToken cancellationToken)
            => TypedResults.Ok(await sortedResultsService.GetAllAsync(cancellationToken)));

        group.MapGet("/{id}", async Task<IResult> (int id, ISortedResultsService sortedResultsService, CancellationToken cancellationToken) =>
            {
                var foundDto = await sortedResultsService.GetById(id, cancellationToken);

                return foundDto == null ? TypedResults.NotFound() : TypedResults.Ok(foundDto);
            });

        group.MapPost("/", async (SortedResultsCreateDto createDto, ISortedResultsService sortedResultsService, CancellationToken cancellationToken)
            => await sortedResultsService.CreateAsync(createDto, cancellationToken));

        group.MapDelete("/{id}", async (int id, ISortedResultsService sortedResultsService, CancellationToken cancellationToken)
            => await sortedResultsService.DeleteAsync(id, cancellationToken));

        return app;
    }
}
