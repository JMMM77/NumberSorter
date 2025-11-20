using System.Diagnostics;
using NumberSorter.Data.Interfaces;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mapper;
using NumberSorter.Shared.Models;

namespace NumberSorter.Services.Services;

internal class SortedNumbersService(ISortedNumbersRespository sortedNumbersRepository) : ISortedNumbersService
{
    private readonly ISortedNumbersRespository _sortedNumbersRepository = sortedNumbersRepository;

    /// <summary>
    /// Retrieves all sorted numbers asynchronously.
    /// </summary>
    /// <returns>A list of SortedNumbersViewModel.</returns>
    public async Task<IEnumerable<SortedNumbersViewModel>> GetAllAsync()
    {
        var dbModels = await _sortedNumbersRepository.GetAllAsync();

        return dbModels.Select(SortedNumbersMapper.ToViewModel);
    }

    /// <summary>
    /// Retrieves a sorted number by its ID asynchronously.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers.</param>
    /// <returns>The SortedNumbersViewModel with the specified ID.</returns>
    public async Task<SortedNumbersViewModel?> GetById(int sortedNumbersId)
    {

        var sortedNumbersViewModel = await _sortedNumbersRepository.GetById(sortedNumbersId);

        return sortedNumbersViewModel?.ToViewModel();
    }

    /// <summary>
    /// Creates a new sorted number asynchronously.
    /// </summary>
    /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel to create.</param>
    /// <returns>The created SortedNumbersViewModel.</returns>
    public async Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel)
    {
        var sortedNumbers = sortedNumbersViewModel.ToEntity();

        await _sortedNumbersRepository.CreateAsync(sortedNumbers);
        await _sortedNumbersRepository.SaveChangesAsync();

        return sortedNumbers.ToViewModel();
    }

    /// <summary>
    /// Deletes a sorted number asynchronously by its ID.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(int sortedNumbersId)
    {
        var sortedNumbersToDelete = await _sortedNumbersRepository.GetById(sortedNumbersId);

        if (sortedNumbersToDelete != null)
        {
            _sortedNumbersRepository.Delete(sortedNumbersToDelete);

            return await _sortedNumbersRepository.SaveChangesAsync();
        }

        return true;
    }

    /// <summary>
    /// Sorts a list of numbers based on the sorting criteria provided in the SortedNumbersViewModel.
    /// </summary>
    /// <param name="sortedNumbersViewModel">The SortedNumbersViewModel containing sorting criteria.</param>
    /// <returns>The SortedNumbersViewModel with sorted values and sort time.</returns>
    public SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel)
    {
        var initalValuesListed = sortedNumbersViewModel.InitialValues.Split(",").Select(int.Parse);
        var sortedValues = Enumerable.Empty<int>();

        Stopwatch stopWatch = new();

        if (sortedNumbersViewModel.IsAscending)
        {
            stopWatch.Start();

            sortedValues = initalValuesListed.Order();

            stopWatch.Stop();
        }
        else
        {
            stopWatch.Start();

            sortedValues = initalValuesListed.OrderByDescending(num => num);

            stopWatch.Stop();
        }

        sortedNumbersViewModel.SortedValues = sortedValues;
        sortedNumbersViewModel.SortTime = stopWatch.Elapsed;

        return sortedNumbersViewModel;
    }
}
