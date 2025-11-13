using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NumberSorter.Services.Interfaces;
using NumberSorter.Shared.Models;

namespace NumberSorter.WebUI.Controllers;

public class SortedNumbersController(ISortedNumbersService sortedNumbersService) : Controller
{
    private readonly ISortedNumbersService _sortedNumbersService = sortedNumbersService;

    /// <summary>
    /// Action method to retrieve all SortedNumbersViewModel asynchronously and render them in the Index view.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var model = await _sortedNumbersService.GetAllAsync();

        foreach (var item in model)
        {
            item.InitialValues = item.InitialValues.Length > 20 ? item.InitialValues[..20] + "..." : item.InitialValues;
            item.SortedValuesString = string.Join(",", item.SortedValues);
            item.SortedValuesString = item.SortedValuesString.Length > 20 ? item.SortedValuesString[..20] + "..." : item.SortedValuesString;
        }

        return this.View(model);
    }

    /// <summary>
    /// Action method to export all SortedNumbers data to a JSON file asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> ExportToJsonAsync()
    {
        var allSortedValues = await _sortedNumbersService.GetAllAsync();
        var json = JsonSerializer.Serialize(allSortedValues);

        return this.File(Encoding.UTF8.GetBytes(json), "application/json", "sorted_numbers.json");
    }

    [HttpGet]
    public async Task<IActionResult> DetailsAsync(int id)
    {
        var model = await _sortedNumbersService.GetById(id);

        return this.View(model);
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
    /// If the model state is not valid, returns the Create view with the provided sortedNumbers.
    /// If the item is created successfully, returns the Details view with the created sortedNumbers.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SortedNumbersViewModel sortedNumbers)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(sortedNumbers);
        }

        sortedNumbers = _sortedNumbersService.CalculateSortedList(sortedNumbers);

        sortedNumbers = await _sortedNumbersService.CreateAsync(sortedNumbers);

        return this.View("Details", sortedNumbers);
    }

    /// <summary>
    /// Action method to display the confirmation page for deleting a SortedNumbersViewModel asynchronously.
    /// </summary>
    /// <param name="id">The ID of the SortedNumbersViewModel to delete.</param>
    /// <returns>A task that represents the asynchronous operation and returns a view result.</returns>
    [HttpGet]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var model = await _sortedNumbersService.GetById(id);
        return this.View(model);
    }

    /// <summary>
    /// Action method to handle the HTTP POST request for deleting a SortedNumbersViewModel asynchronously.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbersViewModel to delete.</param>
    /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
    [HttpPost]
    public async Task<IActionResult> DeleteAsync(SortedNumbersViewModel sortedNumbers)
    {
        await _sortedNumbersService.DeleteAsync(sortedNumbers.Id);

        return this.RedirectToAction("Index");
    }
}
