using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HVACrate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CompressedBuildingEnvelopeInhertianceTablesIntoBuildingEnvelopesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ZOrientationCoefficient",
                table: "BuildingEnvelopes");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 16, 59, 34, 854, DateTimeKind.Unspecified).AddTicks(4817), new TimeSpan(0, 0, 0, 0, 0)),
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
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 16, 59, 34, 854, DateTimeKind.Unspecified).AddTicks(4567), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was created",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7382), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<string>(
                name: "BuildingEnvelopeType",
                table: "BuildingEnvelopes",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "BuildingEnvelopes",
                type: "text",
                nullable: true,
                comment: "The direction of the building envelope, e.g., North, East");

            migrationBuilder.AddColumn<double>(
                name: "GroundWaterLength",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "Length of the floor's contact with groundwater (m)");

            migrationBuilder.AddColumn<double>(
                name: "GroundWaterTemperature",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "Temperature of groundwater under the floor (°C)");

            migrationBuilder.AddColumn<double>(
                name: "JointLength",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "Length of joints surrounding the opening, used in heat loss calculation (m)");

            migrationBuilder.AddColumn<string>(
                name: "OuterWall_Direction",
                table: "BuildingEnvelopes",
                type: "text",
                nullable: true,
                comment: "The direction of the building envelope, e.g., North, East");

            migrationBuilder.AddColumn<bool>(
                name: "ShouldReduceHeatingArea",
                table: "BuildingEnvelopes",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                comment: "Indicates if heat transmission area should be reduced by window/door area on this wall");

            migrationBuilder.AddColumn<double>(
                name: "ThermalConductivityResistance",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "Resistance to heat flow through the floor structure (W·m²/K)");

            migrationBuilder.AddColumn<double>(
                name: "VentilationCoefficient",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "Air exchange coefficient for ventilation through the opening");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingEnvelopeType",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "GroundWaterLength",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "GroundWaterTemperature",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "JointLength",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "OuterWall_Direction",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "ShouldReduceHeatingArea",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "ThermalConductivityResistance",
                table: "BuildingEnvelopes");

            migrationBuilder.DropColumn(
                name: "VentilationCoefficient",
                table: "BuildingEnvelopes");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 13, 14, 2, 65, DateTimeKind.Unspecified).AddTicks(7566), new TimeSpan(0, 0, 0, 0, 0)),
                comment: "The date when the project was modified for the last time",
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 16, 59, 34, 854, DateTimeKind.Unspecified).AddTicks(4817), new TimeSpan(0, 0, 0, 0, 0)),
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
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 7, 7, 16, 59, 34, 854, DateTimeKind.Unspecified).AddTicks(4567), new TimeSpan(0, 0, 0, 0, 0)),
                oldComment: "The date when the project was created");

            migrationBuilder.AddColumn<double>(
                name: "ZOrientationCoefficient",
                table: "BuildingEnvelopes",
                type: "double precision",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.0,
                comment: "Orientation coefficient (Zo)");

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier"),
                    GroundWaterLength = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Length of the floor's contact with groundwater (m)"),
                    GroundWaterTemperature = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Temperature of groundwater under the floor (°C)"),
                    ThermalConductivityResistance = table.Column<double>(type: "double precision", precision: 18, scale: 2, nullable: false, comment: "Resistance to heat flow through the floor structure (W·m²/K)")
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Building envelope's unique identifier")
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
    }
}
