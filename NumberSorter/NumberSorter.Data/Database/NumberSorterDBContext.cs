using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Database;

public class NumberSorterDBContext(DbContextOptions<NumberSorterDBContext> options) : DbContext(options)
{
    public DbSet<SortedNumbers> SortedNumbers { get; set; }
}
