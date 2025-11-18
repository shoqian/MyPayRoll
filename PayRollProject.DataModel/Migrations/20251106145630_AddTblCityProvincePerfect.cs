using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class AddTblCityProvincePerfect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeoProvinces",
                columns: table => new
                {
                    GeoProvince_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoProvinces", x => x.GeoProvince_ID);
                    table.ForeignKey(
                        name: "FK_GeoProvinces_UsersTbl_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoCounties",
                columns: table => new
                {
                    GeoCounty_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    County_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeoProvince_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCounties", x => x.GeoCounty_ID);
                    table.ForeignKey(
                        name: "FK_GeoCounties_GeoProvinces_GeoProvince_ID",
                        column: x => x.GeoProvince_ID,
                        principalTable: "GeoProvinces",
                        principalColumn: "GeoProvince_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoDistricts",
                columns: table => new
                {
                    GeoDistricts_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Districts_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeoCounty_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoDistricts", x => x.GeoDistricts_ID);
                    table.ForeignKey(
                        name: "FK_GeoDistricts_GeoCounties_GeoCounty_ID",
                        column: x => x.GeoCounty_ID,
                        principalTable: "GeoCounties",
                        principalColumn: "GeoCounty_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoRuralDistricts",
                columns: table => new
                {
                    GeoRuralDistricts_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuralDistricts_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeoDistricts_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoRuralDistricts", x => x.GeoRuralDistricts_ID);
                    table.ForeignKey(
                        name: "FK_GeoRuralDistricts_GeoDistricts_GeoDistricts_ID",
                        column: x => x.GeoDistricts_ID,
                        principalTable: "GeoDistricts",
                        principalColumn: "GeoDistricts_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeoNeighborhoods",
                columns: table => new
                {
                    GeoNeighborhoods_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Neighborhoods_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GeoRuralDistricts_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoNeighborhoods", x => x.GeoNeighborhoods_ID);
                    table.ForeignKey(
                        name: "FK_GeoNeighborhoods_GeoRuralDistricts_GeoRuralDistricts_ID",
                        column: x => x.GeoRuralDistricts_ID,
                        principalTable: "GeoRuralDistricts",
                        principalColumn: "GeoRuralDistricts_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoCounties_GeoProvince_ID",
                table: "GeoCounties",
                column: "GeoProvince_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoDistricts_GeoCounty_ID",
                table: "GeoDistricts",
                column: "GeoCounty_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoNeighborhoods_GeoRuralDistricts_ID",
                table: "GeoNeighborhoods",
                column: "GeoRuralDistricts_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoProvinces_UserID",
                table: "GeoProvinces",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoRuralDistricts_GeoDistricts_ID",
                table: "GeoRuralDistricts",
                column: "GeoDistricts_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoNeighborhoods");

            migrationBuilder.DropTable(
                name: "GeoRuralDistricts");

            migrationBuilder.DropTable(
                name: "GeoDistricts");

            migrationBuilder.DropTable(
                name: "GeoCounties");

            migrationBuilder.DropTable(
                name: "GeoProvinces");
        }
    }
}
