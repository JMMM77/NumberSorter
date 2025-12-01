using NumberSorter.Services.Dtos;

namespace NumberSorter.Services.Interfaces;

public interface ILlmChatService
{
    Task<(bool Success, LlmChatResponseDto? Response)> PromptAsync(LlmChatPromptDto promptDto, CancellationToken cancellationToken);
}