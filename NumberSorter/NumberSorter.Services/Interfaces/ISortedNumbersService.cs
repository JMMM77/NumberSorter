using NumberSorter.Common.Models;

namespace NumberSorter.Services.Services
{
    public interface ISortedNumbersService
    {
        public Task<List<SortedNumbersViewModel>> GetAllAsync();
    }
}
