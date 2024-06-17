using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweatSheet.Server.Migrations
{
    /// <inheritdoc />
    public partial class newDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime", "WorkoutDurationInSeconds" },
                values: new object[] { new DateTime(2024, 4, 13, 12, 48, 48, 633, DateTimeKind.Local).AddTicks(5212), new DateTime(2024, 4, 13, 11, 48, 48, 633, DateTimeKind.Local).AddTicks(5205), 3600 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime", "WorkoutDurationInSeconds" },
                values: new object[] { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });
        }
    }
}
