namespace NumberSorter.WebUI.Models.SortedResults;

public sealed class SortedResultsDetailsViewModel
{
    public required int Id { get; init; }

    public required int[] SortedValues { get; init; }

    public required int[] InitialValues { get; init; }

    public required TimeSpan SortTime { get; init; }

    public required bool IsAscending { get; init; }
}
