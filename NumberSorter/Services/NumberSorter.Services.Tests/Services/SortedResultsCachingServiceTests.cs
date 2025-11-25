using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Options;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Tests.Services;

public class SortedResultsCachingServiceTests
{
    private const string CACHE_PREFIX = "sortedResults";
    private const int EXAMPLE_ID = 1;
    private static readonly string s_exampleCacheKey = $"{CACHE_PREFIX}:{EXAMPLE_ID}";

    private static readonly int[] s_exampleSortedValues = [3, 2, 1];
    private static readonly int[] s_exampleInitialValues = [2, 1, 3];
    private static readonly SortedResultsDetailsDto s_exampleSortedResultDetailsDto =
        new()
        {
            Id = EXAMPLE_ID,
            SortedValues = s_exampleSortedValues,
            InitialValues = s_exampleInitialValues,
            SortTime = TimeSpan.Zero,
            IsAscending = false,
        };

    private static readonly int[] s_exampleSortedValues2 = [4, 5, 6];
    private static readonly int[] s_exampleInitialValues2 = [6, 5, 4];
    private static readonly SortedResultsDetailsDto s_exampleSortedResultDetailsDto2 =
        new()
        {
            Id = EXAMPLE_ID,
            SortedValues = s_exampleSortedValues2,
            InitialValues = s_exampleInitialValues2,
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

    private readonly IDistributedCache _distributedCache;
    private readonly IOptions<DistributedCachingOptions> _distributedCacheOptions;

    public SortedResultsCachingServiceTests()
    {
        _distributedCache = Substitute.For<IDistributedCache>();
        _distributedCacheOptions = Substitute.For<IOptions<DistributedCachingOptions>>();
        _distributedCacheOptions.Value.Returns(new DistributedCachingOptions());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTrue_WhenCacheExistsAndIsValid()
    {
        // Arrange
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(s_exampleSortedResultDetailsDto);

        _distributedCache.GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>()).Returns(jsonBytes);

        var service = this.CreateDefaultService();

        // Act
        var (Success, foundDto) = await service.GetByIdAsync(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.True(Success);
        Assert.NotNull(foundDto);
        AssertDetailsDtoMatch(s_exampleSortedResultDetailsDto, foundDto.Value);

        await _distributedCache.Received(requiredNumberOfCalls: 1).GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>());
        await _distributedCache.DidNotReceive().RemoveAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsFalse_WhenDoesNotExistInCache()
    {
        // Arrange
        _distributedCache.GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var (Success, foundDto) = await service.GetByIdAsync(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.False(Success);
        Assert.Null(foundDto);

        await _distributedCache.Received(requiredNumberOfCalls: 1).GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>());
        await _distributedCache.DidNotReceive().RemoveAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsFalse_WhenExistsCache_ButIsInvalid()
    {
        // Arrange
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes("INVALID_DATA");

        _distributedCache.GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>()).Returns(jsonBytes);

        var service = this.CreateDefaultService();

        // Act
        var (Success, foundDto) = await service.GetByIdAsync(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.False(Success);
        Assert.Null(foundDto);

        await _distributedCache.Received(requiredNumberOfCalls: 1).GetAsync(s_exampleCacheKey, Arg.Any<CancellationToken>());
        await _distributedCache.Received(requiredNumberOfCalls: 1).RemoveAsync(s_exampleCacheKey, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateOrUpdateAsync_UpdatesCache()
    {
        // Arrange
        var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(s_exampleSortedResultDetailsDto);

        _distributedCache.SetAsync(s_exampleCacheKey, jsonBytes, Arg.Any<DistributedCacheEntryOptions>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var service = this.CreateDefaultService();

        // Act
        await service.CreateOrUpdateAsync(s_exampleSortedResultDetailsDto, CancellationToken.None);

        // Assert
        await _distributedCache.Received(requiredNumberOfCalls: 1).SetAsync(s_exampleCacheKey, Arg.Any<byte[]>(), Arg.Any<DistributedCacheEntryOptions>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_DeletesFromCache()
    {
        // Arrange
        _distributedCache.RemoveAsync(s_exampleCacheKey, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        var service = this.CreateDefaultService();

        // Act
        await service.DeleteAsync(EXAMPLE_ID, CancellationToken.None);

        // Assert
        await _distributedCache.Received(requiredNumberOfCalls: 1).RemoveAsync(s_exampleCacheKey, Arg.Any<CancellationToken>());
    }

    private static void AssertDetailsDtoMatch(SortedResultsDetailsDto expectedDto, SortedResultsDetailsDto actualDto)
    {
        Assert.Equal(expectedDto.Id, actualDto.Id);
        Assert.Equal(expectedDto.SortedValues, actualDto.SortedValues);
        Assert.Equal(expectedDto.InitialValues, actualDto.InitialValues);
        Assert.Equal(expectedDto.SortTime, actualDto.SortTime);
        Assert.Equal(expectedDto.IsAscending, actualDto.IsAscending);
    }

    private SortedResultsCachingService CreateDefaultService()
        => new(_distributedCache, _distributedCacheOptions);
}
