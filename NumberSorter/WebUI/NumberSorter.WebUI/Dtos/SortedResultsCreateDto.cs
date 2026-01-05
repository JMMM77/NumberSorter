namespace NumberSorter.WebUI.Dtos;

public readonly record struct SortedResultsCreateDto
{
    public readonly int[] InitialValues { get; init; }
    public readonly bool IsAscending { get; init; }
}
