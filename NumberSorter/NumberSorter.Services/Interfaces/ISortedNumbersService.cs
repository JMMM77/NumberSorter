using NumberSorter.Common.Models;

namespace NumberSorter.Services.Interfaces;

public interface ISortedNumbersService
{
    /// <summary>
    /// Retrieves all sorted numbers asynchronously.
    /// </summary>
    /// <returns>A list of SortedNumbersViewModel.</returns>
    Task<List<SortedNumbersViewModel>> GetAllAsync();

    /// <summary>
    /// Retrieves a sorted number by its ID asynchronously.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers.</param>
    /// <returns>The SortedNumbersViewModel with the specified ID.</returns>
    Task<SortedNumbersViewModel> GetById(int sortedNumbersId);

    /// <summary>
    /// Creates a new sorted number asynchronously.
    /// </summary>
    /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel to create.</param>
    /// <returns>The created SortedNumbersViewModel.</returns>
    Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel);

    /// <summary>
    /// Deletes a sorted number asynchronously by its ID.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteAsync(int sortedNumbersId);

    /// <summary>
    /// Sorts a list of numbers based on the sorting criteria provided in the SortedNumbersViewModel.
    /// </summary>
    /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel containing sorting criteria.</param>
    /// <returns>The SortedNumbersViewModel with sorted values and sort time.</returns>
    SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel);
}
