using NumberSorter.Data.Models;

namespace NumberSorter.Data.Interfaces;

public interface ISortedResultsRespository
{
    /// <summary>
    /// Asynchronously adds a new sorted result record to the database.
    /// </summary>
    /// <param name="sortedResults">The <see cref="SortedResults"/> object to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task CreateAsync(SortedResults sortedResults, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves all sorted result records from the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> containing a list of <see cref="SortedResults"/>.</returns>
    Task<List<SortedResults>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a sorted result record by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the sorted result record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> containing the <see cref="SortedResults"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<SortedResults?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a sorted result record from the database.
    /// </summary>
    /// <param name="sortedResults">The <see cref="SortedResults"/> object to delete.</param>
    void Delete(SortedResults sortedResults);

    /// <summary>
    /// Asynchronously saves all changes made in the database context.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> containing <c>true</c> if the changes were successfully saved; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
