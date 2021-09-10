using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordManager.Migrations
{
    public partial class updatedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Record",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Record_UserId",
                table: "Record",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_User_UserId",
                table: "Record",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_User_UserId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_UserId",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Record");
        }
    }
}
