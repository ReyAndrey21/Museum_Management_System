using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNew1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Tours_id_tour",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Discounts_id_discount",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Users_id_users",
                table: "TourBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuides_Users_id_users",
                table: "TourGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideSchedules_TourGuides_id_tour_guide",
                table: "TourGuideSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourGuides_id_tour_guide",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides");

            migrationBuilder.AddColumn<int>(
                name: "DiscountIdDiscount",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides",
                column: "id_users",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DiscountIdDiscount",
                table: "Tickets",
                column: "DiscountIdDiscount");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews",
                column: "id_exhibit",
                principalTable: "Exhibits",
                principalColumn: "id_exhibit",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Tours_id_tour",
                table: "Reviews",
                column: "id_tour",
                principalTable: "Tours",
                principalColumn: "id_tour",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Discounts_DiscountIdDiscount",
                table: "Tickets",
                column: "DiscountIdDiscount",
                principalTable: "Discounts",
                principalColumn: "id_discount");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Discounts_id_discount",
                table: "Tickets",
                column: "id_discount",
                principalTable: "Discounts",
                principalColumn: "id_discount",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets",
                column: "id_ticket_type",
                principalTable: "TicketTypes",
                principalColumn: "id_ticket_type",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Users_id_users",
                table: "TourBookings",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuides_Users_id_users",
                table: "TourGuides",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideSchedules_TourGuides_id_tour_guide",
                table: "TourGuideSchedules",
                column: "id_tour_guide",
                principalTable: "TourGuides",
                principalColumn: "id_tour_guide",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourGuides_id_tour_guide",
                table: "Tours",
                column: "id_tour_guide",
                principalTable: "TourGuides",
                principalColumn: "id_tour_guide",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Tours_id_tour",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Discounts_DiscountIdDiscount",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Discounts_id_discount",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Tours_id_tour",
                table: "TourBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourBookings_Users_id_users",
                table: "TourBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuides_Users_id_users",
                table: "TourGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuideSchedules_TourGuides_id_tour_guide",
                table: "TourGuideSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_TourGuides_id_tour_guide",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DiscountIdDiscount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DiscountIdDiscount",
                table: "Tickets");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides",
                column: "id_users");

            migrationBuilder.AddForeignKey(
                name: "FK_Exhibits_Sections_id_section",
                table: "Exhibits",
                column: "id_section",
                principalTable: "Sections",
                principalColumn: "id_section",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Exhibits_id_exhibit",
                table: "Reviews",
                column: "id_exhibit",
                principalTable: "Exhibits",
                principalColumn: "id_exhibit");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Tours_id_tour",
                table: "Reviews",
                column: "id_tour",
                principalTable: "Tours",
                principalColumn: "id_tour");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Discounts_id_discount",
                table: "Tickets",
                column: "id_discount",
                principalTable: "Discounts",
                principalColumn: "id_discount");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_id_ticket_type",
                table: "Tickets",
                column: "id_ticket_type",
                principalTable: "TicketTypes",
                principalColumn: "id_ticket_type",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TourBookings_Users_id_users",
                table: "TourBookings",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuides_Users_id_users",
                table: "TourGuides",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuideSchedules_TourGuides_id_tour_guide",
                table: "TourGuideSchedules",
                column: "id_tour_guide",
                principalTable: "TourGuides",
                principalColumn: "id_tour_guide",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_TourGuides_id_tour_guide",
                table: "Tours",
                column: "id_tour_guide",
                principalTable: "TourGuides",
                principalColumn: "id_tour_guide",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
