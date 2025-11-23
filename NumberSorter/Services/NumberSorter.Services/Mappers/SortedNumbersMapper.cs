using NumberSorter.Data.Models;
using NumberSorter.Services.Dtos;
using NumberSorter.WebUI.Dtos;

namespace NumberSorter.Services.Mapper;

public static class SortedNumbersMapper
{
    public static SortedNumbersDetailsDto ToDetailsDto(this SortedNumbers sortedNumbers)
        => new()
        {
            Id = sortedNumbers.Id,
            SortedValues = [.. sortedNumbers.SortedValues],
            InitialValues = [.. sortedNumbers.InitialValues.Split(',').Select(int.Parse)],
            SortTime = sortedNumbers.SortTime,
            IsAscending = sortedNumbers.IsAscending,
        };

    public static SortedNumbers ToEntity(this SortedNumbersCreateDto sortedNumbers, int[] sortedValues, TimeSpan sortTime)
        => new()
        {
            SortedValues = sortedValues,
            InitialValues = string.Join(',', sortedNumbers.InitialValues),
            SortTime = sortTime,
            IsAscending = sortedNumbers.IsAscending,
        };
}
