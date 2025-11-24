using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;

namespace NumberSorter.Services.Mappers;

public static class SortedResultsMapper
{
    public static SortedResultsDetailsDto ToDetailsDto(this SortedResults SortedResults)
        => new()
        {
            Id = SortedResults.Id,
            SortedValues = SortedResults.SortedValues,
            InitialValues = SortedResults.InitialValues,
            SortTime = SortedResults.SortTime,
            IsAscending = SortedResults.IsAscending,
        };

    public static SortedResults ToEntity(this SortedResultsCreateDto SortedResults, int[] sortedValues, TimeSpan sortTime)
        => new()
        {
            SortedValues = sortedValues,
            InitialValues = SortedResults.InitialValues,
            SortTime = sortTime,
            IsAscending = SortedResults.IsAscending,
        };
}
