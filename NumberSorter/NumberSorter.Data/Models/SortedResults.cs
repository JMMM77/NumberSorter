namespace NumberSorter.Data.Models;

public class SortedResults
{
    public int Id { get; set; }
    public required int[] SortedValues { get; set; }
    public required int[] InitialValues { get; set; }
    public required TimeSpan SortTime { get; set; }
    public required bool IsAscending { get; set; }
}
