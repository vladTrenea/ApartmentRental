using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApartmentRentalWebApi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    email_confirmed = table.Column<bool>(nullable: false),
                    email_confirmation_token = table.Column<string>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_id",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "apartments",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    area = table.Column<float>(nullable: false),
                    price_per_month = table.Column<float>(nullable: false),
                    nr_of_rooms = table.Column<short>(nullable: false),
                    latitude = table.Column<double>(nullable: false),
                    longitude = table.Column<double>(nullable: false),
                    is_rentable = table.Column<bool>(nullable: false),
                    is_rented = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    RealtorId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apartments", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_apartment_id",
                        column: x => x.RealtorId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_apartments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apartments_RealtorId",
                table: "apartments",
                column: "RealtorId");

            migrationBuilder.CreateIndex(
                name: "IX_apartments_UserId",
                table: "apartments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_RoleId",
                table: "users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apartments");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
