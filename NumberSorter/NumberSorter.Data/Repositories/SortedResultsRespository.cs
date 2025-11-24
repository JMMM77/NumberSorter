using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Database;
using NumberSorter.Data.Interfaces;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Repositories;

///<inheritdoc/>
internal class SortedResultsRespository(NumberSorterDBContext numberSorterDBContext) : ISortedResultsRespository
{
    private readonly NumberSorterDBContext _numberSorterDBContext = numberSorterDBContext;
    private readonly DbSet<SortedResults> _dbSet = numberSorterDBContext.Set<SortedResults>();

    public async Task CreateAsync(SortedResults sortedResults, CancellationToken cancellationToken)
        => await _dbSet.AddAsync(sortedResults, cancellationToken);

    public async Task<List<SortedResults>> GetAllAsync(CancellationToken cancellationToken)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task<SortedResults?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public void Delete(SortedResults sortedResults) => _dbSet.Remove(sortedResults);

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        => await _numberSorterDBContext.SaveChangesAsync(cancellationToken) > 0;
}
