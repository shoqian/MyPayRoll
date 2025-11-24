using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayRollProject.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class repairFiledProvinceIdInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcinceID",
                table: "Province_Tbl",
                newName: "ProvinceId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Province_Tbl",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "Province_Tbl",
                newName: "ProcinceID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Province_Tbl",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
