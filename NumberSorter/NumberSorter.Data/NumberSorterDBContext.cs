using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Configurations;
using NumberSorter.Data.Models;

namespace NumberSorter.Data;

public class NumberSorterDBContext(DbContextOptions<NumberSorterDBContext> options) : DbContext(options)
{
    public DbSet<SortedNumbers> SortedNumbers { get; set; }

    /// <summary>
    /// Configures the SortedNumbers model using the specified modelBuilder.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for the DbContext.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfiguration(new SortedNumbersConfiguration());
}
