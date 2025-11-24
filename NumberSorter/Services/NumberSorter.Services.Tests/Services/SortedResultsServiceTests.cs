using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Tests.Services;

public class SortedResultsServiceTests
{
    private readonly ISortedResultsRespository _sortedResultsRepositorySub;

    public SortedResultsServiceTests()
    {
        _sortedResultsRepositorySub = Substitute.For<ISortedResultsRespository>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDto()
    {
        // Arrange
        int[] initialVal = [3, 2, 1];
        int[] initialVal2 = [6, 5, 4];
        var entities = new List<SortedResults>
        {
            new()
            {
                SortedValues = [1,2,3],
                InitialValues = string.Join(',', initialVal),
                SortTime = TimeSpan.Zero,
                IsAscending = true,
            },
            new()
            {
                SortedValues = [4,5,6],
                InitialValues = string.Join(',', initialVal2),
                SortTime = TimeSpan.Zero,
                IsAscending = true,
            },
        };

        _sortedResultsRepositorySub.GetAllAsync(Arg.Any<CancellationToken>()).Returns(entities);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetAllAsync(cancellationToken: default);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.True(result[0].InitialValues.SequenceEqual(initialVal));
        Assert.True(result[1].InitialValues.SequenceEqual(initialVal2));
    }

    [Fact]
    public async Task GetById_ReturnsMappedDto_WhenEntityExists()
    {
        // Arrange
        int[] initialVal = [3, 2, 1];
        var entity = new SortedResults()
        {
            SortedValues = [1, 2, 3],
            InitialValues = string.Join(',', initialVal),
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).Returns(entity);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1, cancellationToken: default);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Value.InitialValues.SequenceEqual(initialVal));
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
        int[] initialValues = [3, 1, 2];
        var sortedValues = initialValues.Order().ToArray();
        var createDto = new SortedResultsCreateDto { InitialValues = [3, 1, 2], IsAscending = true };

        _sortedResultsRepositorySub.CreateAsync(Arg.Any<SortedResults>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _sortedResultsRepositorySub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.CreateAsync(createDto, cancellationToken: default);

        // Assert
        Assert.Equal(createDto.InitialValues, result.InitialValues);
        Assert.Equal(result.SortedValues, sortedValues);
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
        var entity = new SortedResults()
        {
            SortedValues = [1, 2, 3],
            InitialValues = "3,2,1",
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

        _sortedResultsRepositorySub.GetById(1, Arg.Any<CancellationToken>()).Returns(entity);
        _sortedResultsRepositorySub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, cancellationToken: default);

        // Assert
        Assert.True(result);

        _sortedResultsRepositorySub.Received(1).Delete(entity);
        await _sortedResultsRepositorySub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private SortedResultsService CreateDefaultService()
        => new(_sortedResultsRepositorySub);
}