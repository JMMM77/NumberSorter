using Microsoft.AspNetCore.Mvc;
using NumberSorter.Common.Models;
using NumberSorter.Services.Services;

namespace NumberSorter.WebUI.Controllers
{
    public class SortedNumbersController(ISortedNumbersService sortedNumbersService) : Controller
    {
        private readonly ISortedNumbersService _sortedNumbersService = sortedNumbersService;

        public async Task<IActionResult> IndexAsync()
        {
            List<SortedNumbersViewModel> model = await _sortedNumbersService.GetAllAsync();

            return View(model);
        }
    }
}
