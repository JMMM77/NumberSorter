using NumberSorter.Data.Models;
using NumberSorter.Services.Mapper;
using NumberSorter.Shared.Models;

namespace NumberSorter.Services.Tests.Mapper;

public class SortedNumbersMapperTests
{
    [Fact]
    public void ToViewModel_MapsEntityToViewModelCorrectly()
    {
        // Arrange
        var entity = new SortedNumbers(
            id: 1,
            sortedValues: [1, 2, 3],
            initialValues: "3,2,1",
            sortTime: TimeSpan.FromMilliseconds(50),
            isAscending: true
        );

        // Act
        var viewModel = entity.ToViewModel();

        // Assert
        Assert.Equal(entity.Id, viewModel.Id);
        Assert.Equal(entity.SortedValues, viewModel.SortedValues);
        Assert.Equal(entity.InitialValues, viewModel.InitialValues);
        Assert.Equal(entity.SortTime, viewModel.SortTime);
        Assert.Equal(entity.IsAscending, viewModel.IsAscending);
        Assert.Equal("1, 2, 3", viewModel.SortedValuesString);
    }

    [Fact]
    public void ToEntity_MapsViewModelToEntityCorrectly()
    {
        // Arrange
        var viewModel = new SortedNumbersViewModel
        {
            Id = 2,
            SortedValues = [4, 5, 6],
            InitialValues = "6,5,4",
            SortTime = TimeSpan.FromMilliseconds(100),
            IsAscending = false
        };

        // Act
        var entity = viewModel.ToEntity();

        // Assert
        Assert.Equal(viewModel.Id, entity.Id);
        Assert.Equal(viewModel.SortedValues, entity.SortedValues);
        Assert.Equal(viewModel.InitialValues, entity.InitialValues);
        Assert.Equal(viewModel.SortTime, entity.SortTime);
        Assert.Equal(viewModel.IsAscending, entity.IsAscending);
    }

    [Fact]
    public void ToViewModel_WithEmptySortedValues_ProducesEmptyString()
    {
        // Arrange
        var entity = new SortedNumbers(
            id: 3,
            sortedValues: [],
            initialValues: "",
            sortTime: TimeSpan.Zero,
            isAscending: true
        );

        // Act
        var viewModel = entity.ToViewModel();

        // Assert
        Assert.Empty(viewModel.SortedValuesString);
    }
}