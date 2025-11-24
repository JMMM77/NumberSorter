using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;

namespace NumberSorter.Services.Services;

/// <inheritdoc/>
internal class SortedResultsCachingService(IDistributedCache distributedCache) : ISortedResultsCachingService
{
    private const string CACHE_PREFIX = "sortedResults";
    private static readonly TimeSpan s_expirationRelativeToNow = TimeSpan.FromMinutes(1);

    public async Task<(bool Success, SortedResultsDetailsDto? foundDto)> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = $"{CACHE_PREFIX}:{id}";
        var cachedItem = await distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrWhiteSpace(cachedItem))
        {
            SortedResultsDetailsDto result;

            try
            {
                result = JsonSerializer.Deserialize<SortedResultsDetailsDto>(cachedItem);

                return (true, result);
            }
            catch (Exception)
            {
                await distributedCache.RemoveAsync(cacheKey, cancellationToken);
            }
        }

        return (false, null);
    }

    public async Task CreateOrUpdateAsync(SortedResultsDetailsDto dto, CancellationToken cancellationToken)
        => await distributedCache.SetStringAsync(
            $"{CACHE_PREFIX}:{dto.Id}",
            JsonSerializer.Serialize(dto),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = s_expirationRelativeToNow
            },
            cancellationToken);

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        => await distributedCache.RemoveAsync($"{CACHE_PREFIX}:{id}", cancellationToken);
}
