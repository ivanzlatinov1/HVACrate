using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HVACrate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SpecifiedCountOnlyForOpeningsAndInternalFences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 14, 17, 38, 560, DateTimeKind.Unspecified).AddTicks(2713), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7532), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 14, 17, 38, 560, DateTimeKind.Unspecified).AddTicks(2504), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7304), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "BuildingEnvelopes",
                type: "integer",
                nullable: true,
                comment: "The count of the internal fences",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "The count of the building envelopes");

            migrationBuilder.AddColumn<int>(
                name: "Opening_Count",
                table: "BuildingEnvelopes",
                type: "integer",
                nullable: true,
                comment: "The count of the openings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Opening_Count",
                table: "BuildingEnvelopes");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7532), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 14, 17, 38, 560, DateTimeKind.Unspecified).AddTicks(2713), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7304), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 14, 17, 38, 560, DateTimeKind.Unspecified).AddTicks(2504), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "BuildingEnvelopes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "The count of the building envelopes",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "The count of the internal fences");
        }
    }
}
