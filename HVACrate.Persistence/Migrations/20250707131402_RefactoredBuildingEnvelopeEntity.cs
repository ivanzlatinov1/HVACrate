using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HVACrate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactoredBuildingEnvelopeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustedTemperature",
                table: "Roofs");

            migrationBuilder.DropColumn(
                name: "AdjustedTemperature",
                table: "OuterWalls");

            migrationBuilder.DropColumn(
                name: "AdjustedTemperature",
                table: "Openings");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7566), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 0, 16, 309, DateTimeKind.Unspecified).AddTicks(4437), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7382), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 0, 16, 309, DateTimeKind.Unspecified).AddTicks(4209), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<double>(
                name: "AdjustedTemperature",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "Effective exterior temperature used in thermal transmission calculations (°C)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustedTemperature",
                table: "BuildingEnvelopes");

            migrationBuilder.AddColumn<double>(
                name: "AdjustedTemperature",
                table: "Roofs",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 1.0,
                comment: "Effective exterior temperature used in thermal transmission calculations (°C)");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 0, 16, 309, DateTimeKind.Unspecified).AddTicks(4437), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7566), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 0, 16, 309, DateTimeKind.Unspecified).AddTicks(4209), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7382), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<double>(
                name: "AdjustedTemperature",
                table: "OuterWalls",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 1.0,
                comment: "Effective exterior temperature used in thermal transmission calculations (°C)");

            migrationBuilder.AddColumn<double>(
                name: "AdjustedTemperature",
                table: "Openings",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 1.0,
                comment: "Effective exterior temperature used in thermal transmission calculations (°C)");
        }
    }
}
