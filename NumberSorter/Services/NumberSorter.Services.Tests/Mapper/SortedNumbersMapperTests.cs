using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Helpers;
using NumberSorter.Services.Mapper;

namespace NumberSorter.Services.Tests.Mapper;

public class SortedNumbersMapperTests
{
    [Fact]
    public void ToDto_ReturnsEntityMappedToDto()
    {
        // Arrange
        var entity = new SortedNumbers()
        {
            SortedValues = [1, 2, 3],
            InitialValues = "3,2,1",
            SortTime = TimeSpan.FromMilliseconds(50),
            IsAscending = true,
        };

        // Act
        var dto = entity.ToDetailsDto();

        // Assert
        Assert.Equal(entity.Id, dto.Id);
        Assert.Equal(entity.SortedValues, dto.SortedValues);

        var dtoInitialValuesString = string.Join(',', dto.InitialValues);

        Assert.Equal(entity.InitialValues, dtoInitialValuesString);
        Assert.Equal(entity.SortTime, dto.SortTime);
        Assert.Equal(entity.IsAscending, dto.IsAscending);
    }

    [Fact]
    public void ToEntity_ReturnsDtoMappedToEntity()
    {
        // Arrange
        var dto = new SortedNumbersCreateDto
        {
            InitialValues = [6, 5, 4],
            IsAscending = false,
        };
        var (sortedValues, sortedTime) = SortedNumbersHelper.CalculateSortedList(dto.InitialValues, dto.IsAscending);

        // Act
        var entity = dto.ToEntity(sortedValues, sortedTime);

        // Assert
        Assert.Equal(sortedValues, entity.SortedValues);

        var entityInitialValues = entity.InitialValues.Split(',').Select(int.Parse).ToArray();

        Assert.Equal(dto.InitialValues, entityInitialValues);
        Assert.Equal(sortedTime, entity.SortTime);
        Assert.Equal(dto.IsAscending, entity.IsAscending);
    }
}