using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HVACrate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedBuildingEnvelopeTypesAndOtherProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "BuildingEnvelopes");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "The room's floor");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 11, 38, 21, 510, DateTimeKind.Unspecified).AddTicks(69), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 5, 9, 6, 37, 57, DateTimeKind.Unspecified).AddTicks(4547), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 11, 38, 21, 509, DateTimeKind.Unspecified).AddTicks(9871), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 5, 9, 6, 37, 57, DateTimeKind.Unspecified).AddTicks(4341), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<int>(
                name: "Floors",
                table: "Buildings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "The number of floors in the building");

            migrationBuilder.AddColumn<string>(
                name: "Orientation",
                table: "Buildings",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Is the building landscape or portrait");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BuildingEnvelopes",
                type: "text",
                nullable: false,
                comment: "Type of the building envelope, e.g., OuterWall, Roof, Floor",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Type of the building envelope, e.g., Wall, Roof, Floor");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BuildingEnvelopes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "The count of the building envelopes");

            migrationBuilder.AddColumn<double>(
                name: "Density",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "The density of the building envelope");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "The height of the building envelope");

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "The width of the building envelope");

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier"),
                    ThermalConductivityResistance = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Resistance to heat flow through the floor structure (W·m²/K)"),
                    GroundWaterLength = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Length of the floor's contact with groundwater (m)"),
                    GroundWaterTemperature = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Temperature of groundwater under the floor (°C)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Floors_BuildingEnvelopes_Id",
                        column: x => x.Id,
                        principalTable: "BuildingEnvelopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Internal fences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internal fences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Internal fences_BuildingEnvelopes_Id",
                        column: x => x.Id,
                        principalTable: "BuildingEnvelopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Openings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier"),
                    Direction = table.Column<string>(type: "text", nullable: false, comment: "The direction of the building envelope, e.g., North, East"),
                    AdjustedTemperature = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, defaultValue: 1.0, comment: "Effective exterior temperature used in thermal transmission calculations (°C)"),
                    JointLength = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Length of joints surrounding the opening, used in heat loss calculation (m)"),
                    VentilationCoefficient = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Air exchange coefficient for ventilation through the opening")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Openings_BuildingEnvelopes_Id",
                        column: x => x.Id,
                        principalTable: "BuildingEnvelopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OuterWalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier"),
                    Direction = table.Column<string>(type: "text", nullable: false, comment: "The direction of the building envelope, e.g., North, East"),
                    AdjustedTemperature = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, defaultValue: 1.0, comment: "Effective exterior temperature used in thermal transmission calculations (°C)"),
                    ShouldReduceHeatingArea = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false, comment: "Indicates if heat transmission area should be reduced by window/door area on this wall")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OuterWalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OuterWalls_BuildingEnvelopes_Id",
                        column: x => x.Id,
                        principalTable: "BuildingEnvelopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roofs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier"),
                    AdjustedTemperature = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, defaultValue: 1.0, comment: "Effective exterior temperature used in thermal transmission calculations (°C)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roofs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roofs_BuildingEnvelopes_Id",
                        column: x => x.Id,
                        principalTable: "BuildingEnvelopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "Internal fences");

            migrationBuilder.DropTable(
                name: "Openings");

            migrationBuilder.DropTable(
                name: "OuterWalls");

            migrationBuilder.DropTable(
                name: "Roofs");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Floors",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "BuildingEnvelopes");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 5, 9, 6, 37, 57, DateTimeKind.Unspecified).AddTicks(4547), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 11, 38, 21, 510, DateTimeKind.Unspecified).AddTicks(69), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was modified for the last time");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 5, 9, 6, 37, 57, DateTimeKind.Unspecified).AddTicks(4341), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 11, 38, 21, 509, DateTimeKind.Unspecified).AddTicks(9871), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BuildingEnvelopes",
                type: "text",
                nullable: false,
                comment: "Type of the building envelope, e.g., Wall, Roof, Floor",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Type of the building envelope, e.g., OuterWall, Roof, Floor");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "BuildingEnvelopes",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "The direction of the building envelope, e.g., North, East");
        }
    }
}
