using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberSorter.Data.Migrations
{
    /// <inheritdoc />
    public partial class AscendingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_ascending",
                table: "sorted_numbers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "sorted_numbers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "initial_values", "is_ascending", "sorted_values" },
                values: new object[] { "[12,14,23,24]", true, "[14,24,12,23]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_ascending",
                table: "sorted_numbers");

            migrationBuilder.UpdateData(
                table: "sorted_numbers",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "initial_values", "sorted_values" },
                values: new object[] { "[12,23]", "[12,23]" });
        }
    }
}
