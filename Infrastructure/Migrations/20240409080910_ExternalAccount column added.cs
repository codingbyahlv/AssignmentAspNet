using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExternalAccountcolumnadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.AddColumn<bool>(
                name: "IsExternalAccount",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExternalAccount",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBestseller = table.Column<bool>(type: "bit", nullable: false),
                    LikesInNumbers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikesInPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdvertisingUpdates = table.Column<bool>(type: "bit", nullable: false),
                    DailyNewsLetter = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventUpdates = table.Column<bool>(type: "bit", nullable: false),
                    Podcasts = table.Column<bool>(type: "bit", nullable: false),
                    StartupsWeekly = table.Column<bool>(type: "bit", nullable: false),
                    WeekInReview = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });
        }
    }
}
