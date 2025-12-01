using NumberSorter.WebUI.Interfaces;

namespace NumberSorter.WebUI.Services;

public class LlmChatService(ILlmChatApiClient llmChatApiClient) : ILlmChatService
{
    public async Task<(bool Success, string? PromptResponse)> PromptAsync(string prompt, CancellationToken cancellationToken)
    {
        var (result, response) = await llmChatApiClient.PromptAsync(prompt, cancellationToken);

        if (!result || !response.HasValue)
        {
            return (result, null);
        }

        return (true, response.Value.Response);
    }
}
