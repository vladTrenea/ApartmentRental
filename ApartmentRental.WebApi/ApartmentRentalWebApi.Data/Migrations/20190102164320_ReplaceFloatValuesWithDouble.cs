using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class ReplaceFloatValuesWithDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "price_per_month",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "area",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "price_per_month",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "area",
                table: "apartments",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
