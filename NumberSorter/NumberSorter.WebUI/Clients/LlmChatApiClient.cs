using NumberSorter.WebUI.Dtos;
using NumberSorter.WebUI.Interfaces;

namespace NumberSorter.WebUI.Clients;

public sealed class LlmChatApiClient(HttpClient httpClient, ILogger<SortedResultsApiClient> logger) : ILlmChatApiClient
{
    private const string API_PATH = "llm-chats";

    public async Task<(bool Success, LlmChatResponseDto? Response)> PromptAsync(string prompt, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;

        var promptDto = new LlmChatPromptDto
        {
            Prompt = prompt,
        };

        try
        {
            response = await httpClient.PostAsJsonAsync(CreateApiPath(), promptDto, cancellationToken);
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error))
            {
                logger.LogError(ex, "Error prompting: {Prompt}", prompt);
            }

            return (false, null);
        }

        if (!response.IsSuccessStatusCode)
        {
            return (false, null);
        }

        return (true, await response.Content.ReadFromJsonAsync<LlmChatResponseDto>(cancellationToken));
    }

    private static string CreateApiPath()
        => $"/{API_PATH}";
}
