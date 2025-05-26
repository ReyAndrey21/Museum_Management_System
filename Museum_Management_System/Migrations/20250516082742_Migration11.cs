using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings");

            migrationBuilder.AddColumn<string>(
                name: "position",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings",
                column: "id_tour",
                principalTable: "Tours",
                principalColumn: "id_tour",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings");

            migrationBuilder.DropColumn(
                name: "position",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings",
                column: "id_tour",
                principalTable: "Tours",
                principalColumn: "id_tour",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
