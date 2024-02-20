using NumberSorter.Data.Models;

namespace NumberSorter.Data.Interfaces
{
    public interface ISortedNumbersRespository
    {
        Task AddAsync(SortedNumbers sortedNumbers);
        Task<List<SortedNumbers>> GetAllAsync();
        void Update(SortedNumbers sortedNumbers);
        void Delete(SortedNumbers sortedNumbers);
        Task<bool> SaveChangesAsync();
    }
}
