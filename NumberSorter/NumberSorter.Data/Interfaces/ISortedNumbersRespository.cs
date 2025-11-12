using NumberSorter.Data.Models;

namespace NumberSorter.Data.Interfaces;

public interface ISortedNumbersRespository
{
    /// <summary>
    /// Asynchronously adds a new record representing sorted numbers to the database.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbers object to be added to the database.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateAsync(SortedNumbers sortedNumbers);

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    Task<List<SortedNumbers>> GetAllAsync();

    /// <summary>
    /// Asynchronously retrieves all records of sorted numbers from the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that yields a list of SortedNumbers.</returns>
    Task<SortedNumbers?> GetById(int sortedNumbersId);

    /// <summary>
    /// Updates a record representing sorted numbers in the database.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbers object to be updated.</param>
    void Update(SortedNumbers sortedNumbers);

    /// <summary>
    /// Removes a record representing sorted numbers from the database.
    /// </summary>
    /// <param name="sortedNumbers">The SortedNumbers object to be deleted from the database.</param>
    void Delete(SortedNumbers sortedNumbers);

    /// <summary>
    /// Saves changes made to the database context asynchronously.
    /// </summary>
    /// <returns>If the database has been successfully saved</returns>
    Task<bool> SaveChangesAsync();
}
