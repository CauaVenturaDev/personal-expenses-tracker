using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personalExpensesTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableClientCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "client_Id",
                table: "income",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "client_id",
                table: "expenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_income_ClientId",
                table: "income",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_expenses_ClientId",
                table: "expenses",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_expenses_Client_ClientId",
                table: "expenses",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_income_Client_ClientId",
                table: "income",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expenses_Client_ClientId",
                table: "expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_income_Client_ClientId",
                table: "income");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_income_ClientId",
                table: "income");

            migrationBuilder.DropIndex(
                name: "IX_expenses_ClientId",
                table: "expenses");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "income");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "expenses");
        }
    }
}
