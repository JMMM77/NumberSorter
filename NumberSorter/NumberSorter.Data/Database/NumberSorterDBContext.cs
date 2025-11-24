using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Database;

public class NumberSorterDBContext(DbContextOptions<NumberSorterDBContext> options) : DbContext(options)
{
    public DbSet<SortedResults> SortedResults { get; set; }
}
