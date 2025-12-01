namespace NumberSorter.WebUI.Dtos;

public readonly record struct LlmChatPromptDto
{
    public readonly string Prompt { get; init; }
}
