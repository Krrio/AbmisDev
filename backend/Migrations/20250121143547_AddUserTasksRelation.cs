using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTasksRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dodaj kolumnę UserId jako nullable
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "integer",
                nullable: true);

            // Przypisz UserId dla istniejących rekordów (zakładając, że przypisujesz je do użytkownika testowego o Id = 1)
            migrationBuilder.Sql("UPDATE \"Tasks\" SET \"UserId\" = 1 WHERE \"UserId\" IS NULL");

            // Zmień kolumnę UserId na NOT NULL
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "integer",
                nullable: false);

            // Dodaj indeks i klucz obcy
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");
        }
    }
}
