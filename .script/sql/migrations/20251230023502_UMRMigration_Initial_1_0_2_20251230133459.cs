using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Kr.Carevo.UMR.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UMRMigration_Initial_1_0_2_20251230133459 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "carevo");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateSequence<int>(
                name: "applicationhistoryseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "applicationseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "contactseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "employmentseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "projectseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "skillseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "streakseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "userseq",
                schema: "carevo");

            migrationBuilder.CreateTable(
                name: "skills",
                schema: "carevo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    effective_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    created_by = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_skills_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(200)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(200)", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Address_Line1 = table.Column<string>(type: "varchar", nullable: true),
                    Address_Line2 = table.Column<string>(type: "varchar", nullable: true),
                    Address_Suburb = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address_City = table.Column<string>(type: "varchar(150)", nullable: true),
                    Address_State = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address_PostCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_Coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Users_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "activity_streaks",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    ActivityDate = table.Column<DateTime>(type: "date", nullable: false),
                    ApplicationCount = table.Column<int>(type: "integer", nullable: false),
                    ConsecutiveDayCount = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    VersionStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activity_streaks_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_activity_streaks_users",
                        column: x => x.UserId,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "applications",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Notes = table.Column<string>(type: "varchar", nullable: true),
                    PersonalizedEmploymentData = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_applications_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_applications_users",
                        column: x => x.UserId,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employments",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Company = table.Column<string>(type: "varchar(500)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Logo = table.Column<string>(type: "varchar", nullable: true),
                    Url = table.Column<string>(type: "varchar", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employments_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_employments_users",
                        column: x => x.UserId,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_contacts",
                schema: "carevo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", nullable: false),
                    value = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    VersionStamp = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_contacts_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_contacts_users",
                        column: x => x.user_id,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_skills",
                schema: "carevo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_skills", x => new { x.user_id, x.skill_id });
                    table.ForeignKey(
                        name: "fk_user_skills_skills",
                        column: x => x.skill_id,
                        principalSchema: "carevo",
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_skills_users",
                        column: x => x.user_id,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "application_status_history",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    PreviousStatus = table.Column<string>(type: "varchar(50)", nullable: true),
                    StatusChangedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Notes = table.Column<string>(type: "varchar", nullable: true),
                    ChangedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    Reason = table.Column<string>(type: "varchar(500)", nullable: true),
                    ApplicationId = table.Column<int>(type: "integer", nullable: false),
                    ApplicationId1 = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    VersionStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_status_history_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_status_history_applications_ApplicationId1",
                        column: x => x.ApplicationId1,
                        principalSchema: "carevo",
                        principalTable: "applications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_application_status_history_applications",
                        column: x => x.ApplicationId,
                        principalSchema: "carevo",
                        principalTable: "applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "varchar(500)", nullable: false),
                    Description = table.Column<string>(type: "varchar", nullable: false),
                    EmploymentId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_projects_employments",
                        column: x => x.EmploymentId,
                        principalSchema: "carevo",
                        principalTable: "employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_projects_users_individual",
                        column: x => x.UserId,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_skills",
                schema: "carevo",
                columns: table => new
                {
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_project_skills", x => new { x.project_id, x.skill_id });
                    table.ForeignKey(
                        name: "fk_project_skills_projects",
                        column: x => x.project_id,
                        principalSchema: "carevo",
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_project_skills_skills",
                        column: x => x.skill_id,
                        principalSchema: "carevo",
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_activity_streaks_user_consecutive",
                schema: "carevo",
                table: "activity_streaks",
                columns: new[] { "UserId", "ConsecutiveDayCount" });

            migrationBuilder.CreateIndex(
                name: "idx_activity_streaks_user_date",
                schema: "carevo",
                table: "activity_streaks",
                columns: new[] { "UserId", "ActivityDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_app_history_appid_date",
                schema: "carevo",
                table: "application_status_history",
                columns: new[] { "ApplicationId", "StatusChangedDate" });

            migrationBuilder.CreateIndex(
                name: "IX_application_status_history_ApplicationId1",
                schema: "carevo",
                table: "application_status_history",
                column: "ApplicationId1");

            migrationBuilder.CreateIndex(
                name: "IX_applications_UserId",
                schema: "carevo",
                table: "applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_employments_UserId",
                schema: "carevo",
                table: "employments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_project_skills_skill_id",
                schema: "carevo",
                table: "project_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_EmploymentId",
                schema: "carevo",
                table: "projects",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_UserId",
                schema: "carevo",
                table: "projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_contacts_user_id",
                schema: "carevo",
                table: "user_contacts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_skill_id",
                schema: "carevo",
                table: "user_skills",
                column: "skill_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_streaks",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "application_status_history",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "project_skills",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "user_contacts",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "user_skills",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "applications",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "projects",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "skills",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "employments",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "users",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "applicationhistoryseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "applicationseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "contactseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "employmentseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "projectseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "skillseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "streakseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "userseq",
                schema: "carevo");
        }
    }
}
