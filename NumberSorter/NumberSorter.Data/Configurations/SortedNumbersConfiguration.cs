using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NumberSorter.Data.Models;

namespace NumberSorter.Data.Configurations;

/// <summary>
/// Configures the entity mapping for the SortedNumbers entity.
/// </summary>
public class SortedNumbersConfiguration : IEntityTypeConfiguration<SortedNumbers>
{
    /// <summary>
    /// Configures the entity mapping for SortedNumbers.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<SortedNumbers> builder)
    {
        builder.HasKey(e => e.Id)
            .HasName("id");

        builder.Property(e => e.Id)
                .HasColumnName("id");

        builder.Property(e => e.SortedValues)
                .HasColumnName("sorted_values")
                .IsRequired();

        builder.Property(e => e.InitialValues)
                .HasColumnName("initial_values")
                .IsRequired();

        builder.Property(e => e.SortTime)
                .HasColumnName("sort_time");

        builder.Property(e => e.IsAscending)
                .HasColumnName("is_ascending");

        builder.ToTable("sorted_numbers");
    }
}
