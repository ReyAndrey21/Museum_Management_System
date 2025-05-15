using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
