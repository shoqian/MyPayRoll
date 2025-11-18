using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGeoTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_UsersTbl_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_UsersTbl_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_UsersTbl_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_UsersTbl_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_UsersTbl_UserID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoProvinces_UsersTbl_UserID",
                table: "GeoProvinces");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "GeoRuralDistricts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GeoCounty_ID",
                table: "GeoRuralDistricts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeoProvince_ID",
                table: "GeoRuralDistricts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GeoRuralDistricts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City_Name",
                table: "GeoNeighborhoods",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "GeoNeighborhoods",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GeoCounty_ID",
                table: "GeoNeighborhoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeoDistricts_ID",
                table: "GeoNeighborhoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeoProvince_ID",
                table: "GeoNeighborhoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GeoNeighborhoods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "GeoDistricts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GeoProvince_ID",
                table: "GeoDistricts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GeoDistricts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "GeoCounties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "GeoCounties",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Operation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateBefore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAfter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diff = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoRuralDistricts_GeoCounty_ID",
                table: "GeoRuralDistricts",
                column: "GeoCounty_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoRuralDistricts_GeoProvince_ID",
                table: "GeoRuralDistricts",
                column: "GeoProvince_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoRuralDistricts_UserID",
                table: "GeoRuralDistricts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoNeighborhoods_GeoCounty_ID",
                table: "GeoNeighborhoods",
                column: "GeoCounty_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoNeighborhoods_GeoDistricts_ID",
                table: "GeoNeighborhoods",
                column: "GeoDistricts_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoNeighborhoods_GeoProvince_ID",
                table: "GeoNeighborhoods",
                column: "GeoProvince_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoNeighborhoods_UserID",
                table: "GeoNeighborhoods",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoDistricts_GeoProvince_ID",
                table: "GeoDistricts",
                column: "GeoProvince_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoDistricts_UserID",
                table: "GeoDistricts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GeoCounties_UserID",
                table: "GeoCounties",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_UsersTbl_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_UsersTbl_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_UsersTbl_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_UsersTbl_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_UsersTbl_UserID",
                table: "Cities",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoCounties_UsersTbl_UserID",
                table: "GeoCounties",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoDistricts_GeoProvinces_GeoProvince_ID",
                table: "GeoDistricts",
                column: "GeoProvince_ID",
                principalTable: "GeoProvinces",
                principalColumn: "GeoProvince_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoDistricts_UsersTbl_UserID",
                table: "GeoDistricts",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoNeighborhoods_GeoCounties_GeoCounty_ID",
                table: "GeoNeighborhoods",
                column: "GeoCounty_ID",
                principalTable: "GeoCounties",
                principalColumn: "GeoCounty_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoNeighborhoods_GeoDistricts_GeoDistricts_ID",
                table: "GeoNeighborhoods",
                column: "GeoDistricts_ID",
                principalTable: "GeoDistricts",
                principalColumn: "GeoDistricts_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoNeighborhoods_GeoProvinces_GeoProvince_ID",
                table: "GeoNeighborhoods",
                column: "GeoProvince_ID",
                principalTable: "GeoProvinces",
                principalColumn: "GeoProvince_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoNeighborhoods_UsersTbl_UserID",
                table: "GeoNeighborhoods",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoProvinces_UsersTbl_UserID",
                table: "GeoProvinces",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoRuralDistricts_GeoCounties_GeoCounty_ID",
                table: "GeoRuralDistricts",
                column: "GeoCounty_ID",
                principalTable: "GeoCounties",
                principalColumn: "GeoCounty_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoRuralDistricts_GeoProvinces_GeoProvince_ID",
                table: "GeoRuralDistricts",
                column: "GeoProvince_ID",
                principalTable: "GeoProvinces",
                principalColumn: "GeoProvince_ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoRuralDistricts_UsersTbl_UserID",
                table: "GeoRuralDistricts",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_UsersTbl_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_UsersTbl_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_UsersTbl_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_UsersTbl_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_UsersTbl_UserID",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoCounties_UsersTbl_UserID",
                table: "GeoCounties");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoDistricts_GeoProvinces_GeoProvince_ID",
                table: "GeoDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoDistricts_UsersTbl_UserID",
                table: "GeoDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoNeighborhoods_GeoCounties_GeoCounty_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoNeighborhoods_GeoDistricts_GeoDistricts_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoNeighborhoods_GeoProvinces_GeoProvince_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoNeighborhoods_UsersTbl_UserID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoProvinces_UsersTbl_UserID",
                table: "GeoProvinces");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoRuralDistricts_GeoCounties_GeoCounty_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoRuralDistricts_GeoProvinces_GeoProvince_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropForeignKey(
                name: "FK_GeoRuralDistricts_UsersTbl_UserID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_GeoRuralDistricts_GeoCounty_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropIndex(
                name: "IX_GeoRuralDistricts_GeoProvince_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropIndex(
                name: "IX_GeoRuralDistricts_UserID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropIndex(
                name: "IX_GeoNeighborhoods_GeoCounty_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_GeoNeighborhoods_GeoDistricts_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_GeoNeighborhoods_GeoProvince_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_GeoNeighborhoods_UserID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropIndex(
                name: "IX_GeoDistricts_GeoProvince_ID",
                table: "GeoDistricts");

            migrationBuilder.DropIndex(
                name: "IX_GeoDistricts_UserID",
                table: "GeoDistricts");

            migrationBuilder.DropIndex(
                name: "IX_GeoCounties_UserID",
                table: "GeoCounties");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "GeoRuralDistricts");

            migrationBuilder.DropColumn(
                name: "GeoCounty_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropColumn(
                name: "GeoProvince_ID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GeoRuralDistricts");

            migrationBuilder.DropColumn(
                name: "City_Name",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "GeoCounty_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "GeoDistricts_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "GeoProvince_ID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GeoNeighborhoods");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "GeoDistricts");

            migrationBuilder.DropColumn(
                name: "GeoProvince_ID",
                table: "GeoDistricts");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GeoDistricts");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "GeoCounties");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "GeoCounties");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_UsersTbl_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_UsersTbl_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_UsersTbl_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_UsersTbl_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_UsersTbl_UserID",
                table: "Cities",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoProvinces_UsersTbl_UserID",
                table: "GeoProvinces",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
