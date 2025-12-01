using Microsoft.AspNetCore.Mvc;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Models.LlmChats;

namespace NumberSorter.WebUI.Controllers;

public class LlmChatsController(ILlmChatService llmChatService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken) => this.View(new LlmChatsIndexViewModel() { Prompt = "" });

    [HttpPost]
    public async Task<IActionResult> IndexAsync(LlmChatsIndexViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(viewModel);
        }

        var (wasSuccess, response) = await llmChatService.PromptAsync(viewModel.Prompt, cancellationToken);
        viewModel.Response = response;

        return !wasSuccess
            ? this.Problem(
                detail: "Failed to prompt Llm.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(viewModel);
    }
}
