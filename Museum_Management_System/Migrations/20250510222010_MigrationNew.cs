using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class MigrationNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserIdUsers",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_UserIdUsers",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuides_Users_UserIdUsers",
                table: "TourGuides");

            migrationBuilder.DropIndex(
                name: "IX_TourGuides_UserIdUsers",
                table: "TourGuides");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_UserIdUsers",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserIdUsers",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserIdUsers",
                table: "TourGuides");

            migrationBuilder.DropColumn(
                name: "UserIdUsers",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "UserIdUsers",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides",
                column: "id_users");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_id_users",
                table: "Tickets",
                column: "id_users");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_id_users",
                table: "Reviews",
                column: "id_users");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews",
                column: "id_users",
                principalTable: "Users",
                principalColumn: "id_users",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_id_users",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Users_id_users",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_TourGuides_Users_id_users",
                table: "TourGuides");

            migrationBuilder.DropIndex(
                name: "IX_TourGuides_id_users",
                table: "TourGuides");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_id_users",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_id_users",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "UserIdUsers",
                table: "TourGuides",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUsers",
                table: "Tickets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserIdUsers",
                table: "Reviews",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_UserIdUsers",
                table: "TourGuides",
                column: "UserIdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserIdUsers",
                table: "Tickets",
                column: "UserIdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserIdUsers",
                table: "Reviews",
                column: "UserIdUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserIdUsers",
                table: "Reviews",
                column: "UserIdUsers",
                principalTable: "Users",
                principalColumn: "id_users");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Users_UserIdUsers",
                table: "Tickets",
                column: "UserIdUsers",
                principalTable: "Users",
                principalColumn: "id_users");

            migrationBuilder.AddForeignKey(
                name: "FK_TourGuides_Users_UserIdUsers",
                table: "TourGuides",
                column: "UserIdUsers",
                principalTable: "Users",
                principalColumn: "id_users");
        }
    }
}
