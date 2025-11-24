namespace NumberSorter.Data.Models;

public class SortedResults
{
    public int Id { get; set; }
    public required IEnumerable<int> SortedValues { get; set; }
    public required string InitialValues { get; set; }
    public required TimeSpan SortTime { get; set; }
    public required bool IsAscending { get; set; }
}
