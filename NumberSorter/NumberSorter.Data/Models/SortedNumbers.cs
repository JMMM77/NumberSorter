namespace NumberSorter.Data.Models;

public class SortedNumbers(int id, IEnumerable<int> sortedValues, string initialValues, TimeSpan sortTime, bool isAscending)
{
    public int Id { get; set; } = id;

    /// <summary>
    /// The ordered list of values
    /// </summary>
    public IEnumerable<int> SortedValues { get; set; } = sortedValues;

    /// <summary>
    /// The initial values that the user has inputed
    /// </summary>
    public string InitialValues { get; set; } = initialValues;

    /// <summary>
    /// The amount of time it took for the list to get sorted
    /// </summary>
    public TimeSpan SortTime { get; set; } = sortTime;

    /// <summary>
    /// Records which direction the list was being ordered in
    /// </summary>
    public bool IsAscending { get; set; } = isAscending;
}
