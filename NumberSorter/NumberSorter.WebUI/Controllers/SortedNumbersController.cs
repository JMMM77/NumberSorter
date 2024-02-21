using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using NumberSorter.Common.Models;
using NumberSorter.Services.Services;

namespace NumberSorter.WebUI.Controllers
{
    public class SortedNumbersController(ISortedNumbersService sortedNumbersService) : Controller
    {
        private readonly ISortedNumbersService _sortedNumbersService = sortedNumbersService;

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<SortedNumbersViewModel> model = await _sortedNumbersService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _sortedNumbersService.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(SortedNumbersViewModel sortedNumbers)
        {
            await _sortedNumbersService.DeleteAsync(sortedNumbers.Id);

            return RedirectToAction("Index");
        }
    }
}
