namespace NumberSorter.Services.Dtos;

public readonly record struct SortedNumbersCreateDto
{
    public readonly int[] InitialValues { get; init; }
    public readonly bool IsAscending { get; init; }
}
