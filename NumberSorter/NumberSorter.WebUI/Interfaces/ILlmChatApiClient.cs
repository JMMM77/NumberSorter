using NumberSorter.WebUI.Dtos;

namespace NumberSorter.WebUI.Interfaces;

public interface ILlmChatApiClient
{
    Task<(bool Success, LlmChatResponseDto? Response)> PromptAsync(string prompt, CancellationToken cancellationToken);
}