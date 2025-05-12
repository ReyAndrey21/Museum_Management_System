using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews",
                column: "id_exhibit",
                principalTable: "Exhibits",
                principalColumn: "id_exhibit",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews",
                column: "id_exhibit",
                principalTable: "Exhibits",
                principalColumn: "id_exhibit",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
