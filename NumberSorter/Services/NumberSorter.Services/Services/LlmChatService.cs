using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;
using OllamaSharp;
using OllamaSharp.Models;

namespace NumberSorter.Services.Services;

internal sealed class LlmChatService(IOllamaApiClient ollamaApiClient) : ILlmChatService
{
    public async Task<(bool Success, LlmChatResponseDto? Response)> PromptAsync(LlmChatPromptDto promptDto, CancellationToken cancellationToken)
    {
        var request = new GenerateRequest()
        {
            Prompt = promptDto.Prompt,
        };

        var response = await ollamaApiClient.GenerateAsync(request, cancellationToken).StreamToEndAsync();

        if (response is null)
        {
            return (false, null);
        }

        var responseDto = new LlmChatResponseDto()
        {
            Response = response.Response,
        };

        return (true, responseDto);
    }
}
