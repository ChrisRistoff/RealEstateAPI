using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RealEstateAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    areaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    schools = table.Column<string>(type: "text", nullable: false),
                    shops = table.Column<string>(type: "text", nullable: false),
                    kindergardens = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areas", x => x.areaId);
                });

            migrationBuilder.CreateTable(
                name: "apartments",
                columns: table => new
                {
                    apartmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    areaId = table.Column<int>(type: "integer", nullable: false),
                    postedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    postcode = table.Column<string>(type: "text", nullable: false),
                    sqrFeet = table.Column<int>(type: "integer", nullable: false),
                    rooms = table.Column<int>(type: "integer", nullable: false),
                    bathrooms = table.Column<int>(type: "integer", nullable: false),
                    parkingSpaces = table.Column<int>(type: "integer", nullable: false),
                    furnished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apartments", x => x.apartmentId);
                    table.ForeignKey(
                        name: "FK_apartments_areas_areaId",
                        column: x => x.areaId,
                        principalTable: "areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    houseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    areaId = table.Column<int>(type: "integer", nullable: false),
                    postedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    postcode = table.Column<string>(type: "text", nullable: false),
                    sqrFeet = table.Column<int>(type: "integer", nullable: false),
                    rooms = table.Column<int>(type: "integer", nullable: false),
                    bathrooms = table.Column<int>(type: "integer", nullable: false),
                    parkingSpaces = table.Column<int>(type: "integer", nullable: false),
                    furnished = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.houseId);
                    table.ForeignKey(
                        name: "FK_houses_areas_areaId",
                        column: x => x.areaId,
                        principalTable: "areas",
                        principalColumn: "areaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apartments_areaId",
                table: "apartments",
                column: "areaId");

            migrationBuilder.CreateIndex(
                name: "IX_houses_areaId",
                table: "houses",
                column: "areaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apartments");

            migrationBuilder.DropTable(
                name: "houses");

            migrationBuilder.DropTable(
                name: "areas");
        }
    }
}
