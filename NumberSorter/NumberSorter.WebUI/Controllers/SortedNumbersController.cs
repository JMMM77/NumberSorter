using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using NumberSorter.Common.Models;
using NumberSorter.Services.Services;
using System.Text;

namespace NumberSorter.WebUI.Controllers
{
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
            List<SortedNumbersViewModel> model = await _sortedNumbersService.GetAllAsync();

            return View(model);
        }

        /// <summary>
        /// Action method to export all SortedNumbers data to a JSON file asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and returns an IActionResult.</returns>
        [HttpPost]
        public async Task<IActionResult> ExportToJsonAsync()
        {
            List<SortedNumbersViewModel> allSortedValues = await _sortedNumbersService.GetAllAsync();

            var json = JsonConvert.SerializeObject(allSortedValues, Formatting.Indented);
            return File(Encoding.UTF8.GetBytes(json), "application/json", "sorted_numbers.json");
        }

        /// <summary>
        /// Action method to display the form for creating a new SortedNumbersViewModel.
        /// </summary>
        /// <returns>A view result representing the HTML page rendered by the Create.cshtml view.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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
            if (!ModelState.IsValid)
            {
                return View(sortedNumbers);
            }

            sortedNumbers = _sortedNumbersService.CalculateSortedList(sortedNumbers);

            await _sortedNumbersService.CreateAsync(sortedNumbers);

            return View(nameof(Details), sortedNumbers);
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
            return View(model);
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

            return RedirectToAction("Index");
        }
    }
}
