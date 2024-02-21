namespace NumberSorter.Data.Models
{
    public class SortedNumbers(int id, IEnumerable<int> sortedValues, IEnumerable<int> initalValues, TimeSpan sortTime)
    {
        public int Id { get; set; } = id;
        public IEnumerable<int> SortedValues { get; set; } = sortedValues;
        public IEnumerable<int> InitalValues { get; set; } = initalValues;

        public TimeSpan SortTime { get; set; } = sortTime;
    }
}
