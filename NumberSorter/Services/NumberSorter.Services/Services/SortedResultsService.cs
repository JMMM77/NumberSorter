using NumberSorter.Data.Interfaces;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Helpers;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mappers;

namespace NumberSorter.Services.Services;

/// <inheritdoc/>
internal class SortedResultsService(ISortedResultsCachingService sortedResultsCache, ISortedResultsRespository sortedResultsRepository) : ISortedResultsService
{
    private const string CACHE_PREFIX = "sortedResults";

    public async Task<SortedResultsDetailsDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dbModels = await sortedResultsRepository.GetAllAsync(cancellationToken);
        var dtos = dbModels.Select(SortedResultsMapper.ToDetailsDto).ToArray();

        return [.. dtos];
    }

    public async Task<SortedResultsDetailsDto?> GetById(int id, CancellationToken cancellationToken)
    {
        var (wasSuccess, cachedResult) = await sortedResultsCache.GetByIdAsync(id, cancellationToken);

        if (wasSuccess && cachedResult is not null)
        {
            return cachedResult;
        }

        var entity = await sortedResultsRepository.GetByIdAsync(id, cancellationToken);
        var dto = entity?.ToDetailsDto();

        if (!dto.HasValue)
        {
            return null;
        }

        await sortedResultsCache.CreateOrUpdateAsync(dto.Value, cancellationToken);

        return dto;
    }

    public async Task<SortedResultsDetailsDto> CreateAsync(SortedResultsCreateDto createDto, CancellationToken cancellationToken)
    {
        var (sortedValues, sortTime) = SortNumbersHelper.CalculateSortedList(createDto.InitialValues, createDto.IsAscending);
        var sortedResults = createDto.ToEntity(sortedValues, sortTime);

        await sortedResultsRepository.CreateAsync(sortedResults, cancellationToken);
        await sortedResultsRepository.SaveChangesAsync(cancellationToken);

        var dto = sortedResults.ToDetailsDto();

        await sortedResultsCache.CreateOrUpdateAsync(dto, cancellationToken);

        return dto;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var resultToDelete = await sortedResultsRepository.GetByIdAsync(id, cancellationToken);

        if (resultToDelete != null)
        {
            sortedResultsRepository.Delete(resultToDelete);

            if (!await sortedResultsRepository.SaveChangesAsync(cancellationToken))
            {
                return false;
            }

            var cacheKey = $"{CACHE_PREFIX}:{id}";

            await sortedResultsCache.DeleteAsync(id, cancellationToken);
        }

        return true;
    }
}
