using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NumberSorter.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sorted_numbers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sorted_values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    initial_values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sort_time = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "sorted_numbers",
                columns: new[] { "id", "initial_values", "sort_time", "sorted_values" },
                values: new object[] { 1, "[12,23]", new TimeSpan(0, 0, 0, 5, 0), "[12,23]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sorted_numbers");
        }
    }
}
