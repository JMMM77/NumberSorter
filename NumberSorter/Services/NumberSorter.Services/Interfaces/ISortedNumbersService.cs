using NumberSorter.Services.Dtos;
using NumberSorter.WebUI.Dtos;

namespace NumberSorter.Services.Interfaces;

public interface ISortedNumbersService
{
    /// <summary>
    /// Retrieves all sorted numbers asynchronously.
    /// </summary>
    /// <returns>A list of SortedNumbersViewModel.</returns>
    Task<SortedNumbersDetailsDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a sorted number by its ID asynchronously.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers.</param>
    /// <returns>The SortedNumbersViewModel with the specified ID.</returns>
    Task<SortedNumbersDetailsDto?> GetById(int sortedNumbersId, CancellationToken cancellationToken);

    /// <summary>
    /// Creates a new sorted number asynchronously.
    /// </summary>
    /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel to create.</param>
    /// <returns>The created SortedNumbersViewModel.</returns>
    Task<SortedNumbersDetailsDto> CreateAsync(SortedNumbersCreateDto sortedNumbersViewModel, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes a sorted number asynchronously by its ID.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteAsync(int sortedNumbersId, CancellationToken cancellationToken);
}
