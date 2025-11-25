using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;
using NumberSorter.Services.Mappers;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Tests.Services;

public class SortedResultsServiceTests
{
    private const int EXAMPLE_ID = 1;

    private static readonly int[] s_exampleSortedValues = [3, 2, 1];
    private static readonly int[] s_exampleInitialValues = [2, 1, 3];
    private static readonly SortedResults s_exampleSortedResult =
        new()
        {
            SortedValues = s_exampleSortedValues,
            InitialValues = s_exampleInitialValues,
            SortTime = TimeSpan.Zero,
            IsAscending = false,
        };

    private static readonly int[] s_exampleSortedValues2 = [4, 5, 6];
    private static readonly int[] s_exampleInitialValues2 = [6, 5, 4];
    private static readonly SortedResults s_exampleSortedResult2 =
        new()
        {
            SortedValues = s_exampleSortedValues2,
            InitialValues = s_exampleInitialValues2,
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

    private readonly ISortedResultsCachingService _sortedResultsCachingSub;
    private readonly ISortedResultsRespository _sortedResultsRepositorySub;

    public SortedResultsServiceTests()
    {
        _sortedResultsCachingSub = Substitute.For<ISortedResultsCachingService>();
        _sortedResultsRepositorySub = Substitute.For<ISortedResultsRespository>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDto()
    {
        // Arrange
        var entities = new List<SortedResults>
        {
            s_exampleSortedResult,
            s_exampleSortedResult2,
        };
        var expectedDetailsDto = entities.Select(SortedResultsMapper.ToDetailsDto).ToArray();

        _sortedResultsRepositorySub.GetAllAsync(Arg.Any<CancellationToken>()).Returns(entities);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.Equal(s_exampleSortedResult.InitialValues, result[0].InitialValues);
        Assert.Equal(s_exampleSortedResult2.InitialValues, result[1].InitialValues);

        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).GetAllAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_HasCache_ReturnsMappedDto_WhenEntityExists_DoesNotCallsRepo()
    {
        // Arrange
        _sortedResultsCachingSub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).Returns((true, s_exampleSortedResult.ToDetailsDto()));

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(s_exampleSortedResult.InitialValues, result.Value.InitialValues);

        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>());
        await _sortedResultsRepositorySub.DidNotReceive().GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.DidNotReceive().CreateOrUpdateAsync(Arg.Any<SortedResultsDetailsDto>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_NotInCache_ReturnsMappedDto_WhenEntityExists_CallsRepo()
    {
        // Arrange
        _sortedResultsCachingSub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).Returns((false, null));
        _sortedResultsRepositorySub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).Returns(s_exampleSortedResult);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(s_exampleSortedResult.InitialValues, result.Value.InitialValues);

        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>());
        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).CreateOrUpdateAsync(Arg.Any<SortedResultsDetailsDto>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedResultsCachingSub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).Returns((false, null));
        _sortedResultsRepositorySub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.Null(result);

        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>());
        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.DidNotReceive().CreateOrUpdateAsync(Arg.Any<SortedResultsDetailsDto>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task CreateAsync_CreatesEntityAndReturnsDto()
    {
        // Arrange
        var createDto = new SortedResultsCreateDto()
        {
            InitialValues = s_exampleInitialValues,
            IsAscending = false,
        };

        _sortedResultsRepositorySub.CreateAsync(Arg.Any<SortedResults>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _sortedResultsRepositorySub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.CreateAsync(createDto, CancellationToken.None);

        // Assert
        Assert.Equal(createDto.InitialValues, result.InitialValues);
        Assert.Equal(s_exampleSortedValues, result.SortedValues);
        Assert.Equal(createDto.IsAscending, result.IsAscending);

        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).CreateAsync(Arg.Any<SortedResults>(), Arg.Any<CancellationToken>());
        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).CreateOrUpdateAsync(Arg.Any<SortedResultsDetailsDto>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedResultsRepositorySub.GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(EXAMPLE_ID, CancellationToken.None);

        // Assert
        Assert.True(result);

        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>());
        _sortedResultsRepositorySub.DidNotReceive().Delete(Arg.Any<SortedResults>());
        await _sortedResultsRepositorySub.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.DidNotReceive().DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityAndReturnsResult_WhenEntityExists()
    {
        // Arrange
        _sortedResultsRepositorySub.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(s_exampleSortedResult);
        _sortedResultsRepositorySub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, CancellationToken.None);

        // Assert
        Assert.True(result);

        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).GetByIdAsync(EXAMPLE_ID, Arg.Any<CancellationToken>());
        _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).Delete(s_exampleSortedResult);
        await _sortedResultsRepositorySub.Received(requiredNumberOfCalls: 1).SaveChangesAsync(Arg.Any<CancellationToken>());
        await _sortedResultsCachingSub.Received(requiredNumberOfCalls: 1).DeleteAsync(Arg.Any<int>(), Arg.Any<CancellationToken>());
    }

    private SortedResultsService CreateDefaultService()
        => new(_sortedResultsCachingSub, _sortedResultsRepositorySub);
}