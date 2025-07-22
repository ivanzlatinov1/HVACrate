using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HVACrate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAreaPropertyOnBuildingEnvelope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7532), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 14, 18, 5, 59, 937, DateTimeKind.Unspecified).AddTicks(9445), new TimeSpan(0, 0, 0, 0, 0)),
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
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 14, 18, 5, 59, 937, DateTimeKind.Unspecified).AddTicks(9227), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<double>(
                name: "Area",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "The area of the building envelope");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "BuildingEnvelopes");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 14, 18, 5, 59, 937, DateTimeKind.Unspecified).AddTicks(9445), new TimeSpan(0, 0, 0, 0, 0)),
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
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 14, 18, 5, 59, 937, DateTimeKind.Unspecified).AddTicks(9227), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 22, 12, 36, 51, 723, DateTimeKind.Unspecified).AddTicks(7304), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");
        }
    }
}
