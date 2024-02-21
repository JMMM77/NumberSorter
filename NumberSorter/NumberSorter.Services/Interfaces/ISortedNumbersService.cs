using NumberSorter.Common.Models;

namespace NumberSorter.Services.Services
{
    public interface ISortedNumbersService
    {
        public Task<List<SortedNumbersViewModel>> GetAllAsync();
        public Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel);
        public SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel);
    }
}
