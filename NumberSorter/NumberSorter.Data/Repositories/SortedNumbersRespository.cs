using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Repositories;

internal class SortedNumbersRespository(NumberSorterDBContext numberSorterDBContext) : ISortedNumbersRespository
{
    private readonly NumberSorterDBContext _numberSorterDBContext = numberSorterDBContext;
    private readonly DbSet<SortedNumbers> _dbSet = numberSorterDBContext.Set<SortedNumbers>();

    /// <summary>
    /// Asynchronously adds a new record representing sorted numbers to the database.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbers object to be added to the database.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CreateAsync(SortedNumbers sortedNumbers) => await _dbSet.AddAsync(sortedNumbers);

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    public async Task<List<SortedNumbers>> GetAllAsync() => await _dbSet.ToListAsync();

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    public async Task<SortedNumbers?> GetById(int sortedNumbersId)
    {
        var test = _dbSet.Where(x => x.Id == sortedNumbersId);

        return await _dbSet.Where(x => x.Id == sortedNumbersId).FirstOrDefaultAsync();
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
    public void Delete(SortedNumbers sortedNumbers) => _dbSet.Remove(sortedNumbers);

    /// <summary>
    /// Saves changes made to the database context asynchronously.
    /// </summary>
    /// <returns>If the database has been successfully saved</returns>
    public async Task<bool> SaveChangesAsync() => await _numberSorterDBContext.SaveChangesAsync() > 0;
}
