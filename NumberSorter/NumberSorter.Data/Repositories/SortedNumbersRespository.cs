using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Repositories
{
    public class SortedNumbersRespository : ISortedNumbersRespository
    {
        private readonly DbContext _numberSorterDBContext;
        private readonly DbSet<SortedNumbers> _dbSet;

        protected SortedNumbersRespository(DbContext numberSorterDBContext)
        {
            _numberSorterDBContext = numberSorterDBContext;
            _dbSet = numberSorterDBContext.Set<SortedNumbers>();
        }

        /// <summary>
        /// Asynchronously adds a new record representing sorted numbers to the database.
        /// </summary>
        /// <param name="sortedNumbers">The SortedNumbers object to be added to the database.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(SortedNumbers sortedNumbers)
        {
            await _dbSet.AddAsync(sortedNumbers);
        }

        /// <summary>
        /// Asynchronously retrieves all records of sorted numbers from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
        public async Task<List<SortedNumbers>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Updates a record representing sorted numbers in the database.
        /// </summary>
        /// <param name="sortedNumbers">The SortedNumbers object to be updated.</param>

        public void Update(SortedNumbers sortedNumbers)
        {
            _dbSet.Attach(sortedNumbers);
            _numberSorterDBContext.Entry(sortedNumbers).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes a record representing sorted numbers from the database.
        /// </summary>
        /// <param name="sortedNumbers">The SortedNumbers object to be deleted from the database.</param>
        public void Delete(SortedNumbers sortedNumbers)
        {
            _dbSet.Remove(sortedNumbers);
        }

        /// <summary>
        /// Saves changes made to the database context asynchronously.
        /// </summary>
        /// <returns>If the database has been successfully saved</returns>
        public async  Task<bool> SaveChangesAsync()
        {
            return await _numberSorterDBContext.SaveChangesAsync() > 0;
        }

    }
}
