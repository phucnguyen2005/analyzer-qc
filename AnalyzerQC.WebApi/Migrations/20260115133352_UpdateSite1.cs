using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyzerQC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSite1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "frequency",
                table: "site",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "frequency",
                table: "site",
                type: "double",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }
    }
}
