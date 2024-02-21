using System.ComponentModel.DataAnnotations;

namespace NumberSorter.Common.Models
{
    public class SortedNumbersViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// The ordered list of values
        /// </summary>
        public IEnumerable<int> SortedValues { get; set; } = [];

        /// <summary>
        /// The initial values that the user has inputed
        /// </summary>
        [Required(ErrorMessage = "Please enter numbers separated by commas.")]
        [RegularExpression(@"^(\d+(\s*,\s*\d+)*)?$", ErrorMessage = "Invalid format. Please enter numbers separated by commas.")]
        public string InitialValues { get; set; } = "";

        /// <summary>
        /// The amount of time it took for the list to get sorted
        /// </summary>
        public TimeSpan SortTime { get; set; }

        /// <summary>
        /// Records which direction the list was being ordered in
        /// </summary>
        [Required]
        public bool IsAscending { get; set; }
    }
}
