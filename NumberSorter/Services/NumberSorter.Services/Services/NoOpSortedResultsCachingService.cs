using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;

namespace NumberSorter.Services.Services;

/// <inheritdoc/>
internal class NoOpSortedResultsCachingService() : ISortedResultsCachingService
{
    public Task<(bool Success, SortedResultsDetailsDto? foundDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
        => Task.FromResult<(bool, SortedResultsDetailsDto?)>((false, null));

    public Task CreateOrUpdateAsync(SortedResultsDetailsDto dto, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
        => Task.CompletedTask;
}
