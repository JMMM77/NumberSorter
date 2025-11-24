using NumberSorter.Data.Interfaces;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Helpers;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mappers;

namespace NumberSorter.Services.Services;

/// <inheritdoc/>
internal class SortedResultsService(ISortedResultsRespository SortedResultsRepository) : ISortedResultsService
{
    private readonly ISortedResultsRespository _sortedResultsRepository = SortedResultsRepository;

    public async Task<SortedResultsDetailsDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbModels = await _sortedResultsRepository.GetAllAsync(cancellationToken);

        return [.. dbModels.Select(SortedResultsMapper.ToDetailsDto)];
    }

    public async Task<SortedResultsDetailsDto?> GetById(int id, CancellationToken cancellationToken)
    {
        var dto = await _sortedResultsRepository.GetById(id, cancellationToken);

        return dto?.ToDetailsDto();
    }

    public async Task<SortedResultsDetailsDto> CreateAsync(SortedResultsCreateDto createDto, CancellationToken cancellationToken)
    {
        var (sortedValues, sortTime) = SortNumbersHelper.CalculateSortedList(createDto.InitialValues, createDto.IsAscending);
        var sortedResults = createDto.ToEntity(sortedValues, sortTime);

        await _sortedResultsRepository.CreateAsync(sortedResults, cancellationToken);
        await _sortedResultsRepository.SaveChangesAsync(cancellationToken);

        return sortedResults.ToDetailsDto();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var resultToDelete = await _sortedResultsRepository.GetById(id, cancellationToken);

        if (resultToDelete != null)
        {
            _sortedResultsRepository.Delete(resultToDelete);

            return await _sortedResultsRepository.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
