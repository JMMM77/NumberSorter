namespace NumberSorter.WebUI.Dtos;

public readonly record struct SortedNumbersCreateDto
{
    public readonly string[] InitialValues { get; init; }
    public readonly bool IsAscending { get; init; }
}
