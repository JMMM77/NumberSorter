namespace NumberSorter.Common.Models
{
    public class SortedNumbersViewModel
    {
        public int Id { get; set; }
        public IEnumerable<int> SortedValues { get; set; } = [];
        public IEnumerable<int> InitalValues { get; set; } = [];

        public TimeSpan SortTime { get; set; }
    }
}
