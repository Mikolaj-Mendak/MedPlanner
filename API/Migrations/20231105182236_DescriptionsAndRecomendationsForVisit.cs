using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionsAndRecomendationsForVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Visits",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reccomendations",
                table: "Visits",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Reccomendations",
                table: "Visits");
        }
    }
}
