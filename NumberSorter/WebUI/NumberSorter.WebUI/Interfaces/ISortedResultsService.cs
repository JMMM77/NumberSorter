using NumberSorter.WebUI.Models.SortedResults;

namespace NumberSorter.WebUI.Interfaces;

public interface ISortedResultsService
{
    /// <summary>
    /// Retrieves all sorted result records.
    /// </summary>
    /// <returns>A tuple indicating success and an array of all sorted result details, or null if none.</returns>
    Task<(bool Success, SortedResultsDetailsViewModel[]? AllDetailsDtos)> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a sorted result record by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the record.</param>
    /// <returns>A tuple indicating success and the details of the record, or null if not found.</returns>
    Task<(bool Success, SortedResultsDetailsViewModel? DetailsDto)> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new sorted result record.
    /// </summary>
    /// <param name="createViewModel">The data for the new record.</param>
    /// <returns>A tuple indicating success and the details of the created record.</returns>
    Task<(bool Success, SortedResultsDetailsViewModel? DetailsDto)> CreateAsync(SortedResultsCreateViewModel createViewModel, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sorted result record by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the record to delete.</param>
    /// <returns>True if the deletion succeeded, false otherwise.</returns>
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}