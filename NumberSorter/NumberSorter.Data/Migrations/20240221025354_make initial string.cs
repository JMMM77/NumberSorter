using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberSorter.Data.Migrations
{
    /// <inheritdoc />
    public partial class makeinitialstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sorted_numbers",
                keyColumn: "id",
                keyValue: 1,
                column: "initial_values",
                value: "12,14,23,24");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sorted_numbers",
                keyColumn: "id",
                keyValue: 1,
                column: "initial_values",
                value: "[12,14,23,24]");
        }
    }
}
