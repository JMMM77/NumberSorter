using NumberSorter.Services.Dtos;

namespace NumberSorter.Services.Interfaces;

public interface ISortedResultsService
{
    /// <summary>
    /// Retrieves all sorted results asynchronously.
    /// </summary>
    /// <returns>An array of <see cref="SortedResultsDetailsDto"/>.</returns>
    Task<SortedResultsDetailsDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a sorted result by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the sorted results.</param>
    /// <returns>The <see cref="SortedResultsDetailsDto"/> with the specified ID.</returns>
    Task<SortedResultsDetailsDto?> GetById(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new sorted result asynchronously.
    /// </summary>
    /// <param name="createDto">The <see cref="SortedResultsCreateDto"/> to create.</param>
    /// <returns>The created <see cref="SortedResultsDetailsDto"/>.</returns>
    Task<SortedResultsDetailsDto> CreateAsync(SortedResultsCreateDto createDto, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sorted result asynchronously by its ID.
    /// </summary>
    /// <param name="id">The ID of the sorted result to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
