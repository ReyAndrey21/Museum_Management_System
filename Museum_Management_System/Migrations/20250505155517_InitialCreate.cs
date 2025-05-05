using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Museum_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    id_discount = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    percentage_discout = table.Column<double>(type: "double precision", nullable: false),
                    beneficiary_category = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.id_discount);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    id_employee = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    hire_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    salary = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.id_employee);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    id_faq = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question = table.Column<string>(type: "text", nullable: true),
                    answer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.id_faq);
                });

            migrationBuilder.CreateTable(
                name: "MuseumSchedules",
                columns: table => new
                {
                    id_museum_schedule = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    day_of_week = table.Column<string>(type: "text", nullable: true),
                    opening_hour = table.Column<TimeSpan>(type: "interval", nullable: false),
                    closing_hour = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuseumSchedules", x => x.id_museum_schedule);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    id_section = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_section = table.Column<string>(type: "text", nullable: true),
                    description_section = table.Column<string>(type: "text", nullable: true),
                    image_section = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.id_section);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id_users = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    profile_picture = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id_users);
                });

            migrationBuilder.CreateTable(
                name: "Exhibits",
                columns: table => new
                {
                    id_exhibit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_exhibit = table.Column<string>(type: "text", nullable: true),
                    description_exhibit = table.Column<string>(type: "text", nullable: true),
                    historical_period = table.Column<string>(type: "text", nullable: true),
                    category_exhibit = table.Column<string>(type: "text", nullable: true),
                    image_exhibit = table.Column<string>(type: "text", nullable: true),
                    id_section = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibits", x => x.id_exhibit);
                    table.ForeignKey(
                        name: "FK_Exhibits_Sections_id_section",
                        column: x => x.id_section,
                        principalTable: "Sections",
                        principalColumn: "id_section",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id_ticket = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ticket_type = table.Column<string>(type: "text", nullable: true),
                    id_users = table.Column<int>(type: "integer", nullable: false),
                    UserIdUsers = table.Column<int>(type: "integer", nullable: true),
                    id_discount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id_ticket);
                    table.ForeignKey(
                        name: "FK_Tickets_Discounts_id_discount",
                        column: x => x.id_discount,
                        principalTable: "Discounts",
                        principalColumn: "id_discount");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserIdUsers",
                        column: x => x.UserIdUsers,
                        principalTable: "Users",
                        principalColumn: "id_users");
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    id_tour_guide = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: true),
                    foreign_languages = table.Column<string>(type: "text", nullable: true),
                    id_users = table.Column<int>(type: "integer", nullable: false),
                    UserIdUsers = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuides", x => x.id_tour_guide);
                    table.ForeignKey(
                        name: "FK_TourGuides_Users_UserIdUsers",
                        column: x => x.UserIdUsers,
                        principalTable: "Users",
                        principalColumn: "id_users");
                });

            migrationBuilder.CreateTable(
                name: "TourGuideSchedules",
                columns: table => new
                {
                    id_tour_guide_schedule = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    day_of_week = table.Column<string>(type: "text", nullable: true),
                    start_hour = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_hour = table.Column<TimeSpan>(type: "interval", nullable: false),
                    id_tour_guide = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuideSchedules", x => x.id_tour_guide_schedule);
                    table.ForeignKey(
                        name: "FK_TourGuideSchedules_TourGuides_id_tour_guide",
                        column: x => x.id_tour_guide,
                        principalTable: "TourGuides",
                        principalColumn: "id_tour_guide",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    id_tour = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    available_spots = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    date_tour = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    hour_tour = table.Column<TimeSpan>(type: "interval", nullable: false),
                    id_tour_guide = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.id_tour);
                    table.ForeignKey(
                        name: "FK_Tours_TourGuides_id_tour_guide",
                        column: x => x.id_tour_guide,
                        principalTable: "TourGuides",
                        principalColumn: "id_tour_guide",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    id_review = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    date_review = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    id_users = table.Column<int>(type: "integer", nullable: false),
                    UserIdUsers = table.Column<int>(type: "integer", nullable: true),
                    id_exhibit = table.Column<int>(type: "integer", nullable: true),
                    id_tour = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.id_review);
                    table.ForeignKey(
                        name: "FK_Reviews_Exhibits_id_exhibit",
                        column: x => x.id_exhibit,
                        principalTable: "Exhibits",
                        principalColumn: "id_exhibit");
                    table.ForeignKey(
                        name: "FK_Reviews_Tours_id_tour",
                        column: x => x.id_tour,
                        principalTable: "Tours",
                        principalColumn: "id_tour");
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserIdUsers",
                        column: x => x.UserIdUsers,
                        principalTable: "Users",
                        principalColumn: "id_users");
                });

            migrationBuilder.CreateTable(
                name: "TourBookings",
                columns: table => new
                {
                    id_tour_booking = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number_tickets = table.Column<int>(type: "integer", nullable: false),
                    id_users = table.Column<int>(type: "integer", nullable: false),
                    id_tour = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourBookings", x => x.id_tour_booking);
                    table.ForeignKey(
                        name: "FK_TourBookings_Tours_id_tour",
                        column: x => x.id_tour,
                        principalTable: "Tours",
                        principalColumn: "id_tour",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourBookings_Users_id_users",
                        column: x => x.id_users,
                        principalTable: "Users",
                        principalColumn: "id_users",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exhibits_id_section",
                table: "Exhibits",
                column: "id_section");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_id_exhibit",
                table: "Reviews",
                column: "id_exhibit");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_id_tour",
                table: "Reviews",
                column: "id_tour");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserIdUsers",
                table: "Reviews",
                column: "UserIdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_id_discount",
                table: "Tickets",
                column: "id_discount");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserIdUsers",
                table: "Tickets",
                column: "UserIdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_TourBookings_id_tour",
                table: "TourBookings",
                column: "id_tour");

            migrationBuilder.CreateIndex(
                name: "IX_TourBookings_id_users",
                table: "TourBookings",
                column: "id_users");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_UserIdUsers",
                table: "TourGuides",
                column: "UserIdUsers");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuideSchedules_id_tour_guide",
                table: "TourGuideSchedules",
                column: "id_tour_guide");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_id_tour_guide",
                table: "Tours",
                column: "id_tour_guide");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "MuseumSchedules");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TourBookings");

            migrationBuilder.DropTable(
                name: "TourGuideSchedules");

            migrationBuilder.DropTable(
                name: "Exhibits");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
