using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopGI.Migrations
{
    /// <inheritdoc />
    public partial class Accountname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "accountname",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accountname",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Product");
        }
    }
}
