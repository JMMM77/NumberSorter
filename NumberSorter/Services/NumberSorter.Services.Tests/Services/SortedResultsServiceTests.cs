using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Tests.Services;

public class SortedResultsServiceTests
{
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

    private readonly ISortedResultsRespository _sortedResultsRepositorySub;

    public SortedResultsServiceTests()
    {
        _sortedResultsRepositorySub = Substitute.For<ISortedResultsRespository>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDto()
    {
        // Arrange
        var exampleSortedResult2 = new SortedResults()
        {
            SortedValues = [4, 5, 6],
            InitialValues = [6, 5, 4],
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

        var entities = new List<SortedResults>
        {
            s_exampleSortedResult,
            exampleSortedResult2,
        };

        _sortedResultsRepositorySub.GetAllAsync(Arg.Any<CancellationToken>()).Returns(entities);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetAllAsync(cancellationToken: default);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.Equal(s_exampleSortedResult.InitialValues, result[0].InitialValues);
        Assert.Equal(exampleSortedResult2.InitialValues, result[1].InitialValues);
    }

    [Fact]
    public async Task GetById_ReturnsMappedDto_WhenEntityExists()
    {
        // Arrange
        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).Returns(s_exampleSortedResult);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1, cancellationToken: default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(s_exampleSortedResult.InitialValues, result.Value.InitialValues);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1, cancellationToken: default);

        // Assert
        Assert.Null(result);
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
        var result = await service.CreateAsync(createDto, cancellationToken: default);

        // Assert
        Assert.Equal(createDto.InitialValues, result.InitialValues);
        Assert.Equal(s_exampleSortedValues, result.SortedValues);
        Assert.Equal(createDto.IsAscending, result.IsAscending);

        await _sortedResultsRepositorySub.Received(1).CreateAsync(Arg.Any<SortedResults>(), Arg.Any<CancellationToken>());
        await _sortedResultsRepositorySub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, cancellationToken: default);

        // Assert
        Assert.True(result);

        _sortedResultsRepositorySub.DidNotReceive().Delete(Arg.Any<SortedResults>());
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityAndReturnsResult_WhenEntityExists()
    {
        // Arrange
        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).Returns(s_exampleSortedResult);
        _sortedResultsRepositorySub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, cancellationToken: default);

        // Assert
        Assert.True(result);

        _sortedResultsRepositorySub.Received(1).Delete(s_exampleSortedResult);
        await _sortedResultsRepositorySub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private SortedResultsService CreateDefaultService()
        => new(_sortedResultsRepositorySub);
}