namespace NumberSorter.WebUI.Interfaces;

public interface ILlmChatService
{
    Task<(bool Success, string? PromptResponse)> PromptAsync(string prompt, CancellationToken cancellationToken);
}