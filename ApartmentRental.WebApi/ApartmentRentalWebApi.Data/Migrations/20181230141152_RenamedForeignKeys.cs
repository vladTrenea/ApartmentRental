using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class RenamedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "users",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_RoleId",
                table: "users",
                newName: "IX_users_role_id");

            migrationBuilder.RenameColumn(
                name: "RealtorId",
                table: "apartments",
                newName: "realtor_id");

            migrationBuilder.RenameIndex(
                name: "IX_apartments_RealtorId",
                table: "apartments",
                newName: "IX_apartments_realtor_id");

            migrationBuilder.AlterColumn<string>(
                name: "email_confirmation_token",
                table: "users",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "users",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_users_role_id",
                table: "users",
                newName: "IX_users_RoleId");

            migrationBuilder.RenameColumn(
                name: "realtor_id",
                table: "apartments",
                newName: "RealtorId");

            migrationBuilder.RenameIndex(
                name: "IX_apartments_realtor_id",
                table: "apartments",
                newName: "IX_apartments_RealtorId");

            migrationBuilder.AlterColumn<string>(
                name: "email_confirmation_token",
                table: "users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
