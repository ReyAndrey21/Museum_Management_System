using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets",
                column: "id_ticket_type",
                principalTable: "TicketTypes",
                principalColumn: "id_ticket_type",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets",
                column: "id_ticket_type",
                principalTable: "TicketTypes",
                principalColumn: "id_ticket_type",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
