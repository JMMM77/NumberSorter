using NumberSorter.Services.Dtos;
using NumberSorter.Services.Interfaces;

namespace NumberSorter.WebApis.Apis;

public static class LlmChatApis
{
    public const string LlmChatApiPath = "llm-chats";

    public static WebApplication AddLlmChatApis(this WebApplication app)
    {
        var group = app.MapGroup($"/{LlmChatApiPath}");

        group.MapPost("", async (LlmChatPromptDto promptDto, ILlmChatService llmChatService, CancellationToken cancellationToken) =>
        {
            var (success, response) = await llmChatService.PromptAsync(promptDto, cancellationToken);

            return !success || response is null
                ? Results.BadRequest(new { message = "Failed to generate response" })
                : Results.Ok(response);
        });

        return app;
    }
}
