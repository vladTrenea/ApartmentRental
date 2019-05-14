using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class ApartmentEntityAdjustements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_rentable",
                table: "apartments");

            migrationBuilder.AlterColumn<int>(
                name: "nr_of_rooms",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "nr_of_rooms",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "is_rentable",
                table: "apartments",
                nullable: false,
                defaultValue: false);
        }
    }
}
