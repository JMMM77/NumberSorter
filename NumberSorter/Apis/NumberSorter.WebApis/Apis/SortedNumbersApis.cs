using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;

namespace NumberSorter.WebApis.Apis;

internal static class SortedNumbersApis
{
    public const string SortedNumbersApiPath = "sorted-numbers";

    public static WebApplication AddSortedNumbersApis(this WebApplication app)
    {
        var group = app.MapGroup($"/{SortedNumbersApiPath}");

        group.MapGet("/", async Task<IResult> (ISortedNumbersService sortedNumbersService, CancellationToken cancellationToken)
            => TypedResults.Ok(await sortedNumbersService.GetAllAsync(cancellationToken)));

        group.MapGet("/{id}", async Task<IResult> (int id, ISortedNumbersService sortedNumbersService, CancellationToken cancellationToken) =>
            {
                var foundViewModel = await sortedNumbersService.GetById(id, cancellationToken);

                return foundViewModel == null ? TypedResults.NotFound() : TypedResults.Ok(foundViewModel);
            });

        group.MapPost("/", async (SortedNumbersCreateDto sortedNumbersViewModel, ISortedNumbersService sortedNumbersService, CancellationToken cancellationToken)
            => await sortedNumbersService.CreateAsync(sortedNumbersViewModel, cancellationToken));

        group.MapDelete("/{id}", async (int id, ISortedNumbersService sortedNumbersService, CancellationToken cancellationToken)
            => await sortedNumbersService.DeleteAsync(id, cancellationToken));

        return app;
    }
}
