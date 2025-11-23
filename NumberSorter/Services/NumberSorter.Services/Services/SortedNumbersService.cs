using NumberSorter.Data.Interfaces;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Helpers;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mapper;
using NumberSorter.WebUI.Dtos;

namespace NumberSorter.Services.Services;

internal class SortedNumbersService(ISortedNumbersRespository sortedNumbersRepository) : ISortedNumbersService
{
    private readonly ISortedNumbersRespository _sortedNumbersRepository = sortedNumbersRepository;

    /// <summary>
    /// Retrieves all sorted numbers asynchronously.
    /// </summary>
    /// <returns>A list of dto.</returns>
    public async Task<SortedNumbersDetailsDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbModels = await _sortedNumbersRepository.GetAllAsync(cancellationToken);

        return [.. dbModels.Select(SortedNumbersMapper.ToDetailsDto)];
    }

    /// <summary>
    /// Retrieves a sorted number by its ID asynchronously.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers.</param>
    /// <returns>The dto with the specified ID.</returns>
    public async Task<SortedNumbersDetailsDto?> GetById(int sortedNumbersId, CancellationToken cancellationToken)
    {
        var dto = await _sortedNumbersRepository.GetById(sortedNumbersId, cancellationToken);

        return dto?.ToDetailsDto();
    }

    /// <summary>
    /// Creates a new sorted number asynchronously.
    /// </summary>
    /// <param name="dto">The dto to create.</param>
    /// <returns>The created dto.</returns>
    public async Task<SortedNumbersDetailsDto> CreateAsync(SortedNumbersCreateDto dto, CancellationToken cancellationToken)
    {
        var (sortedValues, sortTime) = SortedNumbersHelper.CalculateSortedList(dto.InitialValues, dto.IsAscending);
        var sortedNumbers = dto.ToEntity(sortedValues, sortTime);

        await _sortedNumbersRepository.CreateAsync(sortedNumbers, cancellationToken);
        await _sortedNumbersRepository.SaveChangesAsync(cancellationToken);

        return sortedNumbers.ToDetailsDto();
    }

    /// <summary>
    /// Deletes a sorted number asynchronously by its ID.
    /// </summary>
    /// <param name="sortedNumbersId">The ID of the sorted numbers to delete.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> DeleteAsync(int sortedNumbersId, CancellationToken cancellationToken)
    {
        var sortedNumbersToDelete = await _sortedNumbersRepository.GetById(sortedNumbersId, cancellationToken);

        if (sortedNumbersToDelete != null)
        {
            _sortedNumbersRepository.Delete(sortedNumbersToDelete);

            return await _sortedNumbersRepository.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
