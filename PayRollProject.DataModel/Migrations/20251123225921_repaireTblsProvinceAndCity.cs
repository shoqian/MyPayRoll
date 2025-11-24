using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class repaireTblsProvinceAndCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Tbl_Province_Tbl_ProvinceID",
                table: "Cities_Tbl");

            migrationBuilder.DropTable(
                name: "Province_Tbl");

            migrationBuilder.RenameColumn(
                name: "CityID",
                table: "Cities_Tbl",
                newName: "CityId");

            migrationBuilder.CreateTable(
                name: "ProvinceTbl",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceTbl", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_ProvinceTbl_UsersTbl_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceTbl_UserID",
                table: "ProvinceTbl",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Tbl_ProvinceTbl_ProvinceID",
                table: "Cities_Tbl",
                column: "ProvinceID",
                principalTable: "ProvinceTbl",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Tbl_ProvinceTbl_ProvinceID",
                table: "Cities_Tbl");

            migrationBuilder.DropTable(
                name: "ProvinceTbl");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Cities_Tbl",
                newName: "CityID");

            migrationBuilder.CreateTable(
                name: "Province_Tbl",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province_Tbl", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Province_Tbl_UsersTbl_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Province_Tbl_UserID",
                table: "Province_Tbl",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Tbl_Province_Tbl_ProvinceID",
                table: "Cities_Tbl",
                column: "ProvinceID",
                principalTable: "Province_Tbl",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
