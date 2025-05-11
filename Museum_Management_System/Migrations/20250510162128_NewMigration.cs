using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ticket_type",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Tickets",
                newName: "final_price");

            migrationBuilder.AddColumn<int>(
                name: "id_ticket_type",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    id_ticket_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_name = table.Column<string>(type: "text", nullable: true),
                    base_price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.id_ticket_type);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_id_ticket_type",
                table: "Tickets",
                column: "id_ticket_type");

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
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_id_ticket_type",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "id_ticket_type",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "final_price",
                table: "Tickets",
                newName: "price");

            migrationBuilder.AddColumn<string>(
                name: "ticket_type",
                table: "Tickets",
                type: "text",
                nullable: true);
        }
    }
}
