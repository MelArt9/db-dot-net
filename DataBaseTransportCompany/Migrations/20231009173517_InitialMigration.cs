using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataBaseTransportCompany.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    driverId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surname = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    driverLicenseNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    validityPeriodRights = table.Column<DateOnly>(type: "date", nullable: false),
                    experience = table.Column<DateOnly>(type: "date", nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driverId);
                });

            migrationBuilder.CreateTable(
                name: "Stamps",
                columns: table => new
                {
                    stampId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stamps", x => x.stampId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "DriverCategories",
                columns: table => new
                {
                    driverId = table.Column<int>(type: "integer", nullable: false),
                    categoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverCategories", x => new { x.driverId, x.categoryId });
                    table.ForeignKey(
                        name: "FK_DriverCategories_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverCategories_Drivers_driverId",
                        column: x => x.driverId,
                        principalTable: "Drivers",
                        principalColumn: "driverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    modelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    stampId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.modelId);
                    table.ForeignKey(
                        name: "FK_Models_Stamps_stampId",
                        column: x => x.stampId,
                        principalTable: "Stamps",
                        principalColumn: "stampId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportVehicles",
                columns: table => new
                {
                    transportVehicleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    codeRegion = table.Column<int>(type: "integer", nullable: false),
                    dateLastInspection = table.Column<DateOnly>(type: "date", nullable: true),
                    engineNumber = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    numberSeats = table.Column<int>(type: "integer", nullable: false),
                    numberStandingPlaces = table.Column<int>(type: "integer", nullable: true),
                    maxSpeed = table.Column<int>(type: "integer", nullable: false),
                    releaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    modelId = table.Column<int>(type: "integer", nullable: false),
                    typeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportVehicles", x => x.transportVehicleId);
                    table.ForeignKey(
                        name: "FK_TransportVehicles_Models_modelId",
                        column: x => x.modelId,
                        principalTable: "Models",
                        principalColumn: "modelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportVehicles_Types_typeId",
                        column: x => x.typeId,
                        principalTable: "Types",
                        principalColumn: "typeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportVehicleDrivers",
                columns: table => new
                {
                    transportVehicleId = table.Column<int>(type: "integer", nullable: false),
                    driverId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportVehicleDrivers", x => new { x.transportVehicleId, x.driverId });
                    table.ForeignKey(
                        name: "FK_TransportVehicleDrivers_Drivers_driverId",
                        column: x => x.driverId,
                        principalTable: "Drivers",
                        principalColumn: "driverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportVehicleDrivers_TransportVehicles_transportVehicleId",
                        column: x => x.transportVehicleId,
                        principalTable: "TransportVehicles",
                        principalColumn: "transportVehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_title",
                table: "Categories",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverCategories_categoryId",
                table: "DriverCategories",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_driverLicenseNumber_phone",
                table: "Drivers",
                columns: new[] { "driverLicenseNumber", "phone" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_stampId",
                table: "Models",
                column: "stampId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_title",
                table: "Models",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stamps_title",
                table: "Stamps",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportVehicleDrivers_driverId",
                table: "TransportVehicleDrivers",
                column: "driverId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportVehicles_modelId",
                table: "TransportVehicles",
                column: "modelId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportVehicles_number_codeRegion_engineNumber",
                table: "TransportVehicles",
                columns: new[] { "number", "codeRegion", "engineNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportVehicles_typeId",
                table: "TransportVehicles",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Types_title",
                table: "Types",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DriverCategories");

            migrationBuilder.DropTable(
                name: "TransportVehicleDrivers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "TransportVehicles");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Stamps");
        }
    }
}
