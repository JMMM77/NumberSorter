using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NumberSorter.Data.Configurations;
using NumberSorter.Data.Models;

namespace NumberSorter.Data
{
    public class NumberSorterDBContext(DbContextOptions<NumberSorterDBContext> options) : DbContext(options)
    {
        public DbSet<SortedNumbers> SortedNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SortedNumbersConfiguration());
        }
    }
}
