using Microsoft.EntityFrameworkCore.Migrations;

namespace AssetTracking_EF.Migrations
{
    public partial class Creatingnewdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssetType",
                table: "CompanyAssets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetType",
                table: "CompanyAssets");
        }
    }
}
