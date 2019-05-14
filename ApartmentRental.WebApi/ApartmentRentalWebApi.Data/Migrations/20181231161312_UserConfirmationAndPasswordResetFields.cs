using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class UserConfirmationAndPasswordResetFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "confirmed_at",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "password_reset_end_date",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "password_reset_token",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confirmed_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_end_date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "password_reset_token",
                table: "users");
        }
    }
}
