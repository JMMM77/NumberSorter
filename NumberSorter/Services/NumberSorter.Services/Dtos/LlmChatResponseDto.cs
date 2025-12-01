namespace NumberSorter.Services.Dtos;

public readonly record struct LlmChatResponseDto
{
    public readonly string Response { get; init; }
}
