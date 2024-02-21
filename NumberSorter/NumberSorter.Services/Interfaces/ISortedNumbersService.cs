using NumberSorter.Common.Models;

namespace NumberSorter.Services.Services
{
    public interface ISortedNumbersService
    {
        public Task<List<SortedNumbersViewModel>> GetAllAsync();
        public Task<SortedNumbersViewModel> GetById(int sortedNumbersId);
        public Task<SortedNumbersViewModel> CreateAsync(SortedNumbersViewModel sortedNumbersViewModel);
        public Task<bool> DeleteAsync(int sortedNumbersId);
        public SortedNumbersViewModel CalculateSortedList(SortedNumbersViewModel sortedNumbersViewModel);
    }
}
