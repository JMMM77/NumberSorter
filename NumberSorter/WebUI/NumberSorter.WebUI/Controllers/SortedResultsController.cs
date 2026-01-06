using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Models.SortedResults;

namespace NumberSorter.WebUI.Controllers;

public class SortedResultsController(ISortedResultsService sortedResultsService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
    {
        var (wasSuccess, allSortedValues) = await sortedResultsService.GetAllAsync(cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: "Failed to retrieve sorted results.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(allSortedValues);
    }

    [HttpPost]
    public async Task<IActionResult> ExportToJsonAsync(CancellationToken cancellationToken)
    {
        var (wasSuccess, allSortedValues) = await sortedResultsService.GetAllAsync(cancellationToken);

        if (!wasSuccess)
        {
            return this.Problem(
                detail: "Failed to retrieve sorted results.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

        var json = JsonSerializer.Serialize(allSortedValues);

        return this.File(Encoding.UTF8.GetBytes(json), "application/json", "sorted_results.json");
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(int id, CancellationToken cancellationToken)
    {
        var (wasSuccess, viewModel) = await sortedResultsService.GetByIdAsync(id, cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: $"Failed to retrieve sorted results for Id: '{id}'.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(viewModel);
    }

    [HttpGet]
    public IActionResult Create() => this.View();

    [HttpPost]
    public async Task<IActionResult> CreateAsync(SortedResultsCreateViewModel viewModel, CancellationToken cancellationToken)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(viewModel);
        }

        var (wasSuccess, createdModel) = await sortedResultsService.CreateAsync(viewModel, cancellationToken);

        if (!wasSuccess || createdModel is null)
        {
            this.ModelState.AddModelError(string.Empty, "Something went wrong while creating the record. Please try again later.");

            return this.View(viewModel);
        }

        return this.RedirectToAction(
            actionName: "Details",
            routeValues: new { id = createdModel.Id });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var (wasSuccess, viewModel) = await sortedResultsService.GetByIdAsync(id, cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: $"Failed to retrieve sorted results for Id: '{id}'.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(SortedResultsDetailsViewModel sortedresult, CancellationToken cancellationToken)
    {
        await sortedResultsService.DeleteAsync(sortedresult.Id, cancellationToken);

        return this.RedirectToAction("Index");
    }
}
