using Microsoft.EntityFrameworkCore.Migrations;

namespace sullied_data.Migrations
{
    public partial class minorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Location",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Location",
                nullable: true,
                oldClrType: typeof(int));

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

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Location",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Location",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
