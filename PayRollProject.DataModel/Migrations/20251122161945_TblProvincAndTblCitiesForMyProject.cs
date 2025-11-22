using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class TblProvincAndTblCitiesForMyProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Province_Tbl",
                columns: table => new
                {
                    ProcinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProvinceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province_Tbl", x => x.ProcinceID);
                    table.ForeignKey(
                        name: "FK_Province_Tbl_UsersTbl_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cities_Tbl",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities_Tbl", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_Cities_Tbl_Province_Tbl_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province_Tbl",
                        principalColumn: "ProcinceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_Tbl_UsersTbl_UserID",
                        column: x => x.UserID,
                        principalTable: "UsersTbl",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Tbl_ProvinceID",
                table: "Cities_Tbl",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Tbl_UserID",
                table: "Cities_Tbl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Province_Tbl_UserID",
                table: "Province_Tbl",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities_Tbl");

            migrationBuilder.DropTable(
                name: "Province_Tbl");
        }
    }
}
