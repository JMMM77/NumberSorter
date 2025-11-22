using NumberSorter.Services.Interfaces;
using NumberSorter.Shared.Models;

namespace NumberSorter.WebApis.Apis;

internal static class SortedNumbersApis
{
    public const string SortedNumbersApiPath = "sorted-numbers";

    public static WebApplication AddSortedNumbersApis(this WebApplication app)
    {
        var group = app.MapGroup($"/{SortedNumbersApiPath}");

        group.MapGet("/", async (ISortedNumbersService sortedNumbersService)
            => await sortedNumbersService.GetAllAsync());

        group.MapGet("/{id}", async Task<IResult> (int id, ISortedNumbersService sortedNumbersService) =>
            {
                var foundViewModel = await sortedNumbersService.GetById(id);

                return foundViewModel == null ? TypedResults.NotFound() : TypedResults.Ok(foundViewModel);
            });

        group.MapPost("/", async (SortedNumbersViewModel sortedNumbersViewModel, ISortedNumbersService sortedNumbersService)
            => await sortedNumbersService.CreateAsync(sortedNumbersViewModel));

        group.MapDelete("/{id}", async (int id, ISortedNumbersService sortedNumbersService)
            => await sortedNumbersService.DeleteAsync(id));

        return app;
    }
}
