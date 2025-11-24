using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.Services.Mappers;

namespace NumberSorter.Services.Tests.Mapper;

public class SortedResultsMapperTests
{
    private const int EXAMPLE_ID = 1;
    private static readonly int[] s_exampleSortedValues = [1, 2, 3];
    private static readonly int[] s_exampleInitialValues = [2, 1, 3];
    private static readonly int[] s_differentArray = [4, 6, 5];
    private static readonly TimeSpan s_exampleSortTime = TimeSpan.Zero;

    [Theory]
    [MemberData(nameof(ToDto_ReturnsEntityMappedToDto_TestData))]
    public void ToDto_ReturnsEntityMappedToDto(SortedResults sortedResult)
    {
        // Act
        var dto = sortedResult.ToDetailsDto();

        // Assert
        Assert.Equal(sortedResult.Id, dto.Id);
        Assert.Equal(sortedResult.SortedValues, dto.SortedValues);
        Assert.Equal(sortedResult.InitialValues, dto.InitialValues);
        Assert.Equal(sortedResult.SortTime, dto.SortTime);
        Assert.Equal(sortedResult.IsAscending, dto.IsAscending);
    }

    [Theory]
    [MemberData(nameof(ToEntity_ReturnsDtoMappedToEntity_TestData))]
    public void ToEntity_ReturnsDtoMappedToEntity(SortedResultsCreateDto createDto, int[] sortedValues, TimeSpan sortedTime)
    {
        // Act
        var entity = createDto.ToEntity(sortedValues, sortedTime);

        // Assert
        Assert.Equal(sortedValues, entity.SortedValues);
        Assert.Equal(createDto.InitialValues, entity.InitialValues);
        Assert.Equal(sortedTime, entity.SortTime);
        Assert.Equal(createDto.IsAscending, entity.IsAscending);
    }

    public static TheoryData<SortedResults> ToDto_ReturnsEntityMappedToDto_TestData()
        => new()
        {
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID + 1,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = int.MinValue,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = int.MaxValue,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = [],
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_differentArray,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = [],
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_differentArray,
                    SortTime = s_exampleSortTime,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = TimeSpan.MinValue,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = TimeSpan.MaxValue,
                    IsAscending = true,
                }
            },
            {
                new()
                {
                    Id = EXAMPLE_ID,
                    SortedValues = s_exampleSortedValues,
                    InitialValues = s_exampleInitialValues,
                    SortTime = s_exampleSortTime,
                    IsAscending = false,
                }
            },
        };

    public static TheoryData<SortedResultsCreateDto, int[], TimeSpan> ToEntity_ReturnsDtoMappedToEntity_TestData()
        => new()
        {
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = true,
                },
                s_exampleSortedValues,
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = [],
                    IsAscending = true,
                },
                s_exampleSortedValues,
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = s_differentArray,
                    IsAscending = true,
                },
                s_exampleSortedValues,
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = false,
                },
                s_exampleSortedValues,
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = true,
                },
                [],
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = true,
                },
                s_differentArray,
                s_exampleSortTime
            },
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = true,
                },
                s_exampleSortedValues,
                TimeSpan.MinValue
            },
            {
                new()
                {
                    InitialValues = s_exampleInitialValues,
                    IsAscending = true,
                },
                s_exampleSortedValues,
                TimeSpan.MaxValue
            },
        };
}