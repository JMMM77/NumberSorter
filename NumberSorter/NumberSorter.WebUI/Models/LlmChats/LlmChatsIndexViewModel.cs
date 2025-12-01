using System.ComponentModel.DataAnnotations;

namespace NumberSorter.WebUI.Models.LlmChats;

public sealed class LlmChatsIndexViewModel
{
    [Required(ErrorMessage = "Please enter a prompt.")]
    [StringLength(1000, ErrorMessage = "The field must not exceed 1000 characters.")]
    public required string Prompt { get; init; }

    public string? Response { get; set; }
}
