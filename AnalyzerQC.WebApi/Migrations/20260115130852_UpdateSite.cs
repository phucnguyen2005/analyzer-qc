using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyzerQC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "frequency",
                table: "site",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "notification_type",
                table: "site",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "working_days",
                table: "site",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "working_time",
                table: "site",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "frequency",
                table: "site");

            migrationBuilder.DropColumn(
                name: "notification_type",
                table: "site");

            migrationBuilder.DropColumn(
                name: "working_days",
                table: "site");

            migrationBuilder.DropColumn(
                name: "working_time",
                table: "site");
        }
    }
}
