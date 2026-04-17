using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AnalyzerQC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lot",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    lot_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    expiry_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lot", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "model_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    model_group_name = table.Column<string>(type: "longtext", nullable: false),
                    model_group_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_group", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "parameter",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    parameter_name = table.Column<string>(type: "longtext", nullable: false),
                    parameter_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    parameter_units = table.Column<string>(type: "longtext", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parameter", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reagent",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    reagent_name = table.Column<string>(type: "longtext", nullable: false),
                    reagent_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    levels = table.Column<string>(type: "longtext", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reagent", x => x.id);
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
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    working_time = table.Column<string>(type: "longtext", nullable: false),
                    frequency = table.Column<float>(type: "float", nullable: false),
                    notification_type = table.Column<int>(type: "int", nullable: false),
                    working_days = table.Column<string>(type: "longtext", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
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
                    model_name = table.Column<string>(type: "longtext", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false)
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
                name: "assay_limit",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    lot_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    reagent_id = table.Column<int>(type: "int", nullable: false),
                    level = table.Column<string>(type: "longtext", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assay_limit", x => x.id);
                    table.ForeignKey(
                        name: "FK_assay_limit_lot_lot_id",
                        column: x => x.lot_id,
                        principalTable: "lot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assay_limit_reagent_reagent_id",
                        column: x => x.reagent_id,
                        principalTable: "reagent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lot_reagent",
                columns: table => new
                {
                    LotsId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ReagentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lot_reagent", x => new { x.LotsId, x.ReagentsId });
                    table.ForeignKey(
                        name: "FK_lot_reagent_lot_LotsId",
                        column: x => x.LotsId,
                        principalTable: "lot",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lot_reagent_reagent_ReagentsId",
                        column: x => x.ReagentsId,
                        principalTable: "reagent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "model_group_reagent",
                columns: table => new
                {
                    ModelGroupsId = table.Column<int>(type: "int", nullable: false),
                    ReagentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_group_reagent", x => new { x.ModelGroupsId, x.ReagentsId });
                    table.ForeignKey(
                        name: "FK_model_group_reagent_model_group_ModelGroupsId",
                        column: x => x.ModelGroupsId,
                        principalTable: "model_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_model_group_reagent_reagent_ReagentsId",
                        column: x => x.ReagentsId,
                        principalTable: "reagent",
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
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "assay_limit_parameter",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    target = table.Column<float>(type: "float", nullable: false),
                    lower_limit = table.Column<float>(type: "float", nullable: false),
                    upper_limit = table.Column<float>(type: "float", nullable: false),
                    parameter_id = table.Column<int>(type: "int", nullable: false),
                    assay_limit_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    creator_id = table.Column<string>(type: "longtext", nullable: false),
                    last_modification_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modifier_id = table.Column<string>(type: "longtext", nullable: true),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deletion_time = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    deleter_id = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assay_limit_parameter", x => x.id);
                    table.ForeignKey(
                        name: "FK_assay_limit_parameter_assay_limit_assay_limit_id",
                        column: x => x.assay_limit_id,
                        principalTable: "assay_limit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assay_limit_parameter_parameter_parameter_id",
                        column: x => x.parameter_id,
                        principalTable: "parameter",
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
                name: "IX_assay_limit_lot_id",
                table: "assay_limit",
                column: "lot_id");

            migrationBuilder.CreateIndex(
                name: "IX_assay_limit_reagent_id",
                table: "assay_limit",
                column: "reagent_id");

            migrationBuilder.CreateIndex(
                name: "IX_assay_limit_parameter_assay_limit_id",
                table: "assay_limit_parameter",
                column: "assay_limit_id");

            migrationBuilder.CreateIndex(
                name: "IX_assay_limit_parameter_parameter_id",
                table: "assay_limit_parameter",
                column: "parameter_id");

            migrationBuilder.CreateIndex(
                name: "IX_lot_lot_code",
                table: "lot",
                column: "lot_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_lot_reagent_ReagentsId",
                table: "lot_reagent",
                column: "ReagentsId");

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
                name: "IX_model_group_reagent_ReagentsId",
                table: "model_group_reagent",
                column: "ReagentsId");

            migrationBuilder.CreateIndex(
                name: "IX_parameter_parameter_code",
                table: "parameter",
                column: "parameter_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reagent_reagent_code",
                table: "reagent",
                column: "reagent_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_site_site_code",
                table: "site",
                column: "site_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_user_name",
                table: "user",
                column: "user_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "analyzer");

            migrationBuilder.DropTable(
                name: "assay_limit_parameter");

            migrationBuilder.DropTable(
                name: "lot_reagent");

            migrationBuilder.DropTable(
                name: "model_group_reagent");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "model");

            migrationBuilder.DropTable(
                name: "site");

            migrationBuilder.DropTable(
                name: "assay_limit");

            migrationBuilder.DropTable(
                name: "parameter");

            migrationBuilder.DropTable(
                name: "model_group");

            migrationBuilder.DropTable(
                name: "lot");

            migrationBuilder.DropTable(
                name: "reagent");
        }
    }
}
