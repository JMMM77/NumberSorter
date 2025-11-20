using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;
using NumberSorter.Services.Services;
using NumberSorter.Shared.Models;

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
        var entities = new List<SortedNumbers>
        {
            new(1, [1,2,3], "3,2,1", TimeSpan.Zero, true),
            new(2, [4,5,6], "6,5,4", TimeSpan.Zero, true)
        };

        _sortedNumbersSub.GetAllAsync().Returns(entities);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, x => x.InitialValues == "3,2,1");
        Assert.Contains(result, x => x.InitialValues == "6,5,4");
    }

    [Fact]
    public async Task GetById_ReturnsMappedViewModel_WhenEntityExists()
    {
        // Arrange
        var entity = new SortedNumbers(1, [1, 2, 3], "3,2,1", TimeSpan.Zero, true);

        _sortedNumbersSub.GetById(1).Returns(entity);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("3,2,1", result.InitialValues);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedNumbersSub.GetById(1).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.GetById(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesEntityAndReturnsViewModel()
    {
        // Arrange
        var viewModel = new SortedNumbersViewModel { InitialValues = "3,1,2", IsAscending = true };

        _sortedNumbersSub.CreateAsync(Arg.Any<SortedNumbers>()).Returns(Task.CompletedTask);
        _sortedNumbersSub.SaveChangesAsync().Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.CreateAsync(viewModel);

        // Assert
        Assert.NotNull(result);

        await _sortedNumbersSub.Received(1).CreateAsync(Arg.Any<SortedNumbers>());
        await _sortedNumbersSub.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenEntityDoesNotExist()
    {
        // Arrange
        _sortedNumbersSub.GetById(1).ReturnsNull();

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        Assert.True(result);

        _sortedNumbersSub.DidNotReceive().Delete(Arg.Any<SortedNumbers>());
    }

    [Fact]
    public async Task DeleteAsync_DeletesEntityAndReturnsResult_WhenEntityExists()
    {
        // Arrange
        var entity = new SortedNumbers(1, [1, 2, 3], "3,2,1", TimeSpan.Zero, true);

        _sortedNumbersSub.GetById(1).Returns(entity);
        _sortedNumbersSub.SaveChangesAsync().Returns(true);

        var service = this.CreateDefaultService();

        // Act
        var result = await service.DeleteAsync(1);

        // Assert
        Assert.True(result);

        _sortedNumbersSub.Received(1).Delete(entity);
        await _sortedNumbersSub.Received(1).SaveChangesAsync();
    }

    [Fact]
    public void CalculateSortedList_SortsAscendingCorrectly()
    {
        // Arrange
        var viewModel = new SortedNumbersViewModel
        {
            InitialValues = "3,1,2",
            IsAscending = true
        };

        var service = this.CreateDefaultService();

        // Act
        var result = service.CalculateSortedList(viewModel);

        // Assert
        Assert.Equal([1, 2, 3], result.SortedValues);
        Assert.True(result.SortTime > TimeSpan.Zero);
    }

    [Fact]
    public void CalculateSortedList_SortsDescendingCorrectly()
    {
        // Arrange
        var viewModel = new SortedNumbersViewModel
        {
            InitialValues = "3,1,2",
            IsAscending = false
        };

        var service = this.CreateDefaultService();

        // Act
        var result = service.CalculateSortedList(viewModel);

        // Assert
        Assert.Equal([3, 2, 1], result.SortedValues);
        Assert.True(result.SortTime > TimeSpan.Zero);
    }

    private SortedNumbersService CreateDefaultService()
        => new(_sortedNumbersSub);
}