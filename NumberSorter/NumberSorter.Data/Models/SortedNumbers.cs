namespace NumberSorter.Data.Models
{
    public class SortedNumbers
    {
        public int ID { get; set; }
        public IEnumerable<int> SortedValues { get; set; }
        public IEnumerable<int> InitalValues { get; set; }

        public TimeOnly SortTime { get; set; }
    }
}
