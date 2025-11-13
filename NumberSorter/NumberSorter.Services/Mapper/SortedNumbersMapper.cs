using NumberSorter.Data.Models;
using NumberSorter.Shared.Models;

namespace NumberSorter.Services.Mapper;

public static class SortedNumbersMapper
{
    public static SortedNumbersViewModel ToViewModel(this SortedNumbers sortedNumbers)
        => new()
        {
            Id = sortedNumbers.Id,
            SortedValues = sortedNumbers.SortedValues,
            InitialValues = sortedNumbers.InitialValues,
            SortTime = sortedNumbers.SortTime,
            IsAscending = sortedNumbers.IsAscending,
            SortedValuesString = string.Join(", ", sortedNumbers.SortedValues)
        };

    public static SortedNumbers ToEntity(this SortedNumbersViewModel sortedNumbers)
        => new(
            sortedNumbers.Id,
            sortedNumbers.SortedValues,
            sortedNumbers.InitialValues,
            sortedNumbers.SortTime,
            sortedNumbers.IsAscending
        );
}
