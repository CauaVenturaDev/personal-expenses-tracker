using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personalExpensesTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "client_id",
                table: "income",
                type: "uuid",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "client_id",
                table: "expenses",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clients_email",
                table: "clients",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_username",
                table: "clients",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_income_client_id",
                table: "income",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_client_id",
                table: "expenses",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_expenses_client_id",
                table: "expenses",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_incomes_client_id",
                table: "income",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_expenses_client_id",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "fk_incomes_client_id",
                table: "income");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropIndex(
                name: "IX_income_client_id",
                table: "income");

            migrationBuilder.DropIndex(
                name: "IX_expenses_client_id",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "income");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "expenses");
        }
    }
}
