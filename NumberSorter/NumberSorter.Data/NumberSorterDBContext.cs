using Microsoft.EntityFrameworkCore;
using NumberSorter.Data.Models;

namespace NumberSorter.Data
{
    public class NumberSorterDBContext : DbContext
    {
        public string DbPath { get; }

        public NumberSorterDBContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "numberSorter.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer($"Data Source={DbPath}");
    }
}
