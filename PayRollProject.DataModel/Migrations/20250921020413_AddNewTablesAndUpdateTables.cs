using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablesAndUpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries_Tbl",
                table: "Countries_Tbl");

            migrationBuilder.RenameTable(
                name: "Countries_Tbl",
                newName: "Countries");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthOfDate",
                table: "UsersTbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "UsersTbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UsersTbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "UsersTbl",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "MelliCode",
                table: "UsersTbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "UserFlag",
                table: "UsersTbl",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "UserType",
                table: "UsersTbl",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CrateDateTime",
                table: "Countries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Countries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_UserID",
                table: "Countries",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries",
                column: "UserID",
                principalTable: "UsersTbl",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_UsersTbl_UserID",
                table: "Countries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_UserID",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "BirthOfDate",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "MelliCode",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "UserFlag",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "UsersTbl");

            migrationBuilder.DropColumn(
                name: "CrateDateTime",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countries_Tbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries_Tbl",
                table: "Countries_Tbl",
                column: "CountryID");
        }
    }
}
