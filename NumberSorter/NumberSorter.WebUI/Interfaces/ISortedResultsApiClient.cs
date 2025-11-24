using NumberSorter.WebUI.Dtos;

namespace NumberSorter.WebUI.Interfaces;

public interface ISortedResultsApiClient
{
    /// <summary>
    /// Retrieves all sorted result records from the API.
    /// </summary>
    /// <returns>A tuple indicating success and an array of sorted result details, or null if none.</returns>
    Task<(bool Success, SortedResultsDetailsDto[]? DetailsDto)> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a sorted result record by its identifier from the API.
    /// </summary>
    /// <param name="id">The identifier of the record.</param>
    /// <returns>A tuple indicating success and the details of the record, or null if not found.</returns>
    Task<(bool Success, SortedResultsDetailsDto? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new sorted result record via the API.
    /// </summary>
    /// <param name="createDto">The data for the new record.</param>
    /// <returns>A tuple indicating success and the details of the created record.</returns>
    Task<(bool Success, SortedResultsDetailsDto? DetailsDto)> CreateAsync(SortedResultsCreateDto createDto, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sorted result record by its identifier via the API.
    /// </summary>
    /// <param name="id">The identifier of the record to delete.</param>
    /// <returns>True if the deletion succeeded, false otherwise.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}