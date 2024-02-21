using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Configurations
{
    public class SortedNumbersConfiguration : IEntityTypeConfiguration<SortedNumbers>
    {
        public void Configure(EntityTypeBuilder<SortedNumbers> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("id");

            builder.Property(e => e.Id)
                    .HasColumnName("id");

            builder.Property(e => e.SortedValues)
                    .HasColumnName("sorted_values")
                    .IsRequired();

            builder.Property(e => e.InitalValues)
                    .HasColumnName("initial_values")
                    .IsRequired();

            builder.Property(e => e.SortTime)
                    .HasColumnName("sort_time");

            builder.ToTable("sorted_numbers");

            builder.HasData(new SortedNumbers(1, [12, 23], [12, 23], new TimeSpan(0, 0, 5)));
        }
    }
}
