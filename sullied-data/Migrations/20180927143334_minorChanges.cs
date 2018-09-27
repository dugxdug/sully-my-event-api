using Microsoft.EntityFrameworkCore.Migrations;

namespace sullied_data.Migrations
{
    public partial class minorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Location",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YelpId",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "YelpId",
                table: "Location");
        }
    }
}
