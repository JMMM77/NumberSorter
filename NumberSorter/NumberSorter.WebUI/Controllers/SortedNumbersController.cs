using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NumberSorter.WebUI.Interfaces;
using NumberSorter.WebUI.Models.SortedNumbers;

namespace NumberSorter.WebUI.Controllers;

public class SortedNumbersController(ISortedNumbersService sortedNumbersService) : Controller
{
    private readonly ISortedNumbersService _sortedNumbersService = sortedNumbersService;

    /// <summary>
    /// Action method to retrieve all SortedNumbersViewModel asynchronously and render them in the Index view.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpGet]
    public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
    {
        var (wasSuccess, allSortedValues) = await _sortedNumbersService.GetAllAsync(cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: "Failed to retrieve sorted numbers.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(allSortedValues);
    }

    /// <summary>
    /// Action method to export all SortedNumbers data to a JSON file asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> ExportToJsonAsync(CancellationToken cancellationToken)
    {
        var (wasSuccess, allSortedValues) = await _sortedNumbersService.GetAllAsync(cancellationToken);

        if (!wasSuccess)
        {
            return this.Problem(
                detail: "Failed to retrieve sorted numbers.",
                statusCode: StatusCodes.Status500InternalServerError);
        }

        var json = JsonSerializer.Serialize(allSortedValues);

        return this.File(Encoding.UTF8.GetBytes(json), "application/json", "sorted_numbers.json");
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(int id, CancellationToken cancellationToken)
    {
        var (wasSuccess, viewModel) = await _sortedNumbersService.GetByIdAsync(id, cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: $"Failed to retrieve sorted numbers for Id: '{id}'.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(viewModel);
    }

    /// <summary>
    /// Action method to display the form for creating a new SortedNumbersViewModel.
    /// </summary>
    /// <returns>A view result representing the HTML page rendered by the Create.cshtml view.</returns>
    [HttpGet]
    public IActionResult Create() => this.View();

    /// <summary>
    /// Action method to handle the HTTP POST request for creating a new SortedNumbersViewModel asynchronously.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbersViewModel containing the data for the new item.</param>
    /// <returns>
    /// If the allSortedValues state is not valid, returns the Create view with the provided sortedNumbers.
    /// If the item is created successfully, returns the Details view with the created sortedNumbers.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SortedNumbersCreateViewModel sortedNumbers, CancellationToken cancellationToken)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(sortedNumbers);
        }

        var (wasSuccess, createdModel) = await _sortedNumbersService.CreateAsync(sortedNumbers, cancellationToken);

        if (!wasSuccess || createdModel is null)
        {
            this.ModelState.AddModelError(string.Empty, "Something went wrong while creating the record. Please try again later.");

            return this.View(sortedNumbers);
        }

        return this.RedirectToAction(
            actionName: "Details",
            routeValues: new { id = createdModel.Id });
    }

    /// <summary>
    /// Action method to display the confirmation page for deleting a SortedNumbersViewModel asynchronously.
    /// </summary>
    /// <param name="id">The ID of the SortedNumbersViewModel to delete.</param>
    /// <returns>A task that represents the asynchronous operation and returns a view result.</returns>
    [HttpGet]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var (wasSuccess, viewModel) = await _sortedNumbersService.GetByIdAsync(id, cancellationToken);

        return !wasSuccess
            ? this.Problem(
                detail: $"Failed to retrieve sorted numbers for Id: '{id}'.",
                statusCode: StatusCodes.Status500InternalServerError)
            : this.View(viewModel);
    }

    /// <summary>
    /// Action method to handle the HTTP POST request for deleting a SortedNumbersViewModel asynchronously.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbersViewModel to delete.</param>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteAsync(SortedNumbersDetailsViewModel sortedNumbers, CancellationToken cancellationToken)
    {
        await _sortedNumbersService.DeleteAsync(sortedNumbers.Id, cancellationToken);

        return this.RedirectToAction("Index");
    }
}
