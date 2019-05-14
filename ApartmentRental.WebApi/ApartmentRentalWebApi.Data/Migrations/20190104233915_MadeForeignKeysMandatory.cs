using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class MadeForeignKeysMandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_apartments_users_UserId",
                table: "apartments");

            migrationBuilder.DropIndex(
                name: "IX_apartments_UserId",
                table: "apartments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "apartments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "apartments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_apartments_UserId",
                table: "apartments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_apartments_users_UserId",
                table: "apartments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
