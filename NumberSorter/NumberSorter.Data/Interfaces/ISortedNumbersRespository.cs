using NumberSorter.Data.Models;

namespace NumberSorter.Data.Interfaces
{
    public interface ISortedNumbersRespository
    {
        public Task CreateAsync(SortedNumbers sortedNumbers);
        public Task<List<SortedNumbers>> GetAllAsync();
        public void Update(SortedNumbers sortedNumbers);
        public void Delete(SortedNumbers sortedNumbers);
        public Task<bool> SaveChangesAsync();
    }
}
