using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnalyzerQC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitAm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "user");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "user",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "user",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.CreateTable(
                name: "model_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    model_group_name = table.Column<string>(type: "longtext", nullable: false),
                    model_group_code = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_group", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "site",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    site_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    site_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    address = table.Column<string>(type: "longtext", nullable: false),
                    time_zone = table.Column<string>(type: "longtext", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "model",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    model_group_id = table.Column<int>(type: "int", nullable: false),
                    model_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    model_name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model", x => x.id);
                    table.ForeignKey(
                        name: "FK_model_model_group_model_group_id",
                        column: x => x.model_group_id,
                        principalTable: "model_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "analyzer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    model_id = table.Column<int>(type: "int", nullable: false),
                    site_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    serial_number = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_analyzer", x => x.id);
                    table.ForeignKey(
                        name: "FK_analyzer_model_model_id",
                        column: x => x.model_id,
                        principalTable: "model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_analyzer_site_site_id",
                        column: x => x.site_id,
                        principalTable: "site",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_analyzer_model_id",
                table: "analyzer",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "IX_analyzer_site_id",
                table: "analyzer",
                column: "site_id");

            migrationBuilder.CreateIndex(
                name: "IX_model_model_code",
                table: "model",
                column: "model_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_model_model_group_id",
                table: "model",
                column: "model_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_model_group_model_group_code",
                table: "model_group",
                column: "model_group_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_site_site_code",
                table: "site",
                column: "site_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analyzer");

            migrationBuilder.DropTable(
                name: "model");

            migrationBuilder.DropTable(
                name: "site");

            migrationBuilder.DropTable(
                name: "model_group");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
