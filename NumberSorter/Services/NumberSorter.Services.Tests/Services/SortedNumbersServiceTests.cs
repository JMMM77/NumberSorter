using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Services;

namespace NumberSorter.Services.Tests.Services;

public class SortedNumbersServiceTests
{
    private readonly ISortedNumbersRespository _sortedNumbersSub;

    public SortedNumbersServiceTests()
    {
        _sortedNumbersSub = Substitute.For<ISortedNumbersRespository>();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedViewModels()
    {
        // Arrange
        int[] initialVal = [3, 2, 1];
        int[] initialVal2 = [6, 5, 4];
        var entities = new List<SortedNumbers>
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

        _sortedNumbersSub.GetAllAsync(Arg.Any<CancellationToken>()).Returns(entities);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetAllAsync(cancellationToken: default);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.True(result[0].InitialValues.SequenceEqual(initialVal));
        Assert.True(result[1].InitialValues.SequenceEqual(initialVal2));
    }

    [Fact]
    public async Task GetById_ReturnsMappedViewModel_WhenEntityExists()
    {
        // Arrange
        int[] initialVal = [3, 2, 1];
        var entity = new SortedNumbers()
        {
            SortedValues = [1, 2, 3],
            InitialValues = string.Join(',', initialVal),
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

        _sortedNumbersSub.GetById(1, Arg.Any<CancellationToken>()).Returns(entity);

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
        _sortedNumbersSub.GetById(1, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1, cancellationToken: default);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesEntityAndReturnsViewModel()
    {
        // Arrange
        int[] initialValues = [3, 1, 2];
        var sortedValues = initialValues.Order().ToArray();
        var viewModel = new SortedNumbersCreateDto { InitialValues = [3, 1, 2], IsAscending = true };

        _sortedNumbersSub.CreateAsync(Arg.Any<SortedNumbers>(), Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
        _sortedNumbersSub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.CreateAsync(viewModel, cancellationToken: default);

        // Assert
        Assert.Equal(viewModel.InitialValues, result.InitialValues);
        Assert.Equal(result.SortedValues, sortedValues);
        Assert.Equal(viewModel.IsAscending, result.IsAscending);

        await _sortedNumbersSub.Received(1).CreateAsync(Arg.Any<SortedNumbers>(), Arg.Any<CancellationToken>());
        await _sortedNumbersSub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedNumbersSub.GetById(1, Arg.Any<CancellationToken>()).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, cancellationToken: default);

        // Assert
        Assert.True(result);

        _sortedNumbersSub.DidNotReceive().Delete(Arg.Any<SortedNumbers>());
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityAndReturnsResult_WhenEntityExists()
    {
        // Arrange
        var entity = new SortedNumbers()
        {
            SortedValues = [1, 2, 3],
            InitialValues = "3,2,1",
            SortTime = TimeSpan.Zero,
            IsAscending = true,
        };

        _sortedNumbersSub.GetById(1, Arg.Any<CancellationToken>()).Returns(entity);
        _sortedNumbersSub.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1, cancellationToken: default);

        // Assert
        Assert.True(result);

        _sortedNumbersSub.Received(1).Delete(entity);
        await _sortedNumbersSub.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private SortedNumbersService CreateDefaultService()
        => new(_sortedNumbersSub);
}