using System.ComponentModel.DataAnnotations;

namespace NumberSorter.WebUI.Models.SortedResults;

public sealed class SortedResultsCreateViewModel
{
    [Required(ErrorMessage = "Please enter numbers separated by commas.")]
    [RegularExpression(@"^(\d+(\s*,\s*\d+)*)?$", ErrorMessage = "Invalid format. Please enter numbers separated by commas.")]
    [StringLength(3998, ErrorMessage = "The field must not exceed 3998 characters.")]
    public required string InitialValues { get; init; }

    [Required]
    public required bool IsAscending { get; init; }
}
