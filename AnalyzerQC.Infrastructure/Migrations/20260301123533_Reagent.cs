using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnalyzerQC.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Reagent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "user",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "deleter_id",
                table: "user",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_time",
                table: "user",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modification_time",
                table: "user",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modifier_id",
                table: "user",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "site",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "site",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "deleter_id",
                table: "site",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_time",
                table: "site",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "site",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modification_time",
                table: "site",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modifier_id",
                table: "site",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "model_group",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "model_group",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "model",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "model",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "analyzer",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                table: "analyzer",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "deleter_id",
                table: "analyzer",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_time",
                table: "analyzer",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "analyzer",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_modification_time",
                table: "analyzer",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modifier_id",
                table: "analyzer",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "reagent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    reagent_code = table.Column<string>(type: "longtext", nullable: false),
                    reagent_name = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    levels = table.Column<string>(type: "longtext", nullable: false),
                    model_group_id = table.Column<int>(type: "int", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reagent", x => x.id);
                    table.ForeignKey(
                        name: "FK_reagent_model_group_model_group_id",
                        column: x => x.model_group_id,
                        principalTable: "model_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_reagent_model_group_id",
                table: "reagent",
                column: "model_group_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reagent");

            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "user");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "deleter_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "deletion_time",
                table: "user");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_modification_time",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_modifier_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "site");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "site");

            migrationBuilder.DropColumn(
                name: "deleter_id",
                table: "site");

            migrationBuilder.DropColumn(
                name: "deletion_time",
                table: "site");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "site");

            migrationBuilder.DropColumn(
                name: "last_modification_time",
                table: "site");

            migrationBuilder.DropColumn(
                name: "last_modifier_id",
                table: "site");

            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "model_group");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "model_group");

            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "model");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "model");

            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "creator_id",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "deleter_id",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "deletion_time",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "last_modification_time",
                table: "analyzer");

            migrationBuilder.DropColumn(
                name: "last_modifier_id",
                table: "analyzer");
        }
    }
}
