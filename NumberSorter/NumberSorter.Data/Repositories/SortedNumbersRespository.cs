using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Database;
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
    public async Task CreateAsync(SortedNumbers sortedNumbers, CancellationToken cancellationToken)
        => await _dbSet.AddAsync(sortedNumbers, cancellationToken);

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    public async Task<List<SortedNumbers>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbSet.ToListAsync(cancellationToken);

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    public async Task<SortedNumbers?> GetById(int sortedNumbersId, CancellationToken cancellationToken)
        => await _dbSet.Where(x => x.Id == sortedNumbersId).FirstOrDefaultAsync(cancellationToken);

    /// <summary>
    /// Removes a record representing sorted numbers from the database.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbers object to be deleted from the database.</param>
    public void Delete(SortedNumbers sortedNumbers) => _dbSet.Remove(sortedNumbers);

    /// <summary>
    /// Saves changes made to the database context asynchronously.
    /// </summary>
    /// <returns>If the database has been successfully saved</returns>
    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await _numberSorterDBContext.SaveChangesAsync(cancellationToken) > 0;
}
