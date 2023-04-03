using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ship_convenient.Migrations
{
    public partial class initdb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DistanceBackwardVirtual",
                table: "Route",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DistanceForwardVirtual",
                table: "Route",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistanceBackwardVirtual",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "DistanceForwardVirtual",
                table: "Route");
        }
    }
}
