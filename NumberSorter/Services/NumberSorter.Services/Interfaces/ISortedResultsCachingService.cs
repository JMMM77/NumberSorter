using NumberSorter.Services.Dtos;

namespace NumberSorter.Services.Interfaces;

public interface ISortedResultsCachingService
{
    /// <summary>
    /// Retrieves a sorted result from the cache asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the cached sorted result to retrieve.</param>
    /// <returns>
    /// A tuple containing: True if the cache retrieval was successful and the cached <see cref="SortedResultsDetailsDto"/> if found; otherwise, null.
    /// </returns>
    Task<(bool Success, SortedResultsDetailsDto? foundDto)> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates or updates a sorted result in the cache asynchronously.
    /// </summary>
    /// <param name="dto">The <see cref="SortedResultsDetailsDto"/> to cache.</param>
    Task CreateOrUpdateAsync(SortedResultsDetailsDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sorted result from the cache asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the cached sorted result to delete.</param>
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}