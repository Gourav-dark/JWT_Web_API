using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWT_Web_API.Migrations
{
    /// <inheritdoc />
    public partial class Create1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Items_userId",
                table: "Items",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_userId",
                table: "Items",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_userId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_userId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Items");
        }
    }
}
