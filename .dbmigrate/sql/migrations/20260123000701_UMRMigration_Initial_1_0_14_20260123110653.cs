using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Kr.Carevo.UMR.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UMRMigration_Initial_1_0_14_20260123110653 : Migration
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
                name: "employerseq",
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
                name: "userempseq",
                schema: "carevo");

            migrationBuilder.CreateSequence<int>(
                name: "userseq",
                schema: "carevo");

            migrationBuilder.CreateTable(
                name: "employers",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Company = table.Column<string>(type: "varchar(500)", nullable: false),
                    Logo = table.Column<string>(type: "varchar", nullable: true),
                    Url = table.Column<string>(type: "varchar", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employments_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                schema: "carevo",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "varchar(100)", nullable: false),
                    description = table.Column<string>(type: "varchar", nullable: false),
                    effective_date = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
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
                    Dob = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    Address_Line1 = table.Column<string>(type: "varchar", nullable: true),
                    Address_Line2 = table.Column<string>(type: "varchar", nullable: true),
                    Address_Suburb = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address_City = table.Column<string>(type: "varchar(150)", nullable: true),
                    Address_State = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address_PostCode = table.Column<string>(type: "varchar(20)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_Coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_Id", x => x.Id);
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
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
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
                    JobId = table.Column<string>(type: "varchar(100)", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", nullable: false),
                    AppliedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Notes = table.Column<string>(type: "varchar", nullable: true),
                    PersonalizedEmploymentData = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
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
                name: "user_contacts",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(type: "varchar(500)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    VersionStamp = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_contacts_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_user_contacts_users",
                        column: x => x.UserId,
                        principalSchema: "carevo",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_employers",
                schema: "carevo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    EmployerId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_useremployers_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_user_employers_employers",
                        column: x => x.EmployerId,
                        principalSchema: "carevo",
                        principalTable: "employers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_employers_users",
                        column: x => x.UserId,
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_skills", x => new { x.UserId, x.SkillId });
                    table.ForeignKey(
                        name: "fk_user_skills_skills",
                        column: x => x.SkillId,
                        principalSchema: "carevo",
                        principalTable: "skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_skills_users",
                        column: x => x.UserId,
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
                    StatusChangedDate = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    Notes = table.Column<string>(type: "varchar", nullable: true),
                    ChangedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    Reason = table.Column<string>(type: "varchar(500)", nullable: true),
                    ApplicationId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    VersionStamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_application_status_history_Id", x => x.Id);
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
                    UserEmployerId = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<string>(type: "varchar(500)", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_projects_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_projects_useremployers",
                        column: x => x.UserEmployerId,
                        principalSchema: "carevo",
                        principalTable: "user_employers",
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
                name: "IX_applications_UserId",
                schema: "carevo",
                table: "applications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_project_skills_skill_id",
                schema: "carevo",
                table: "project_skills",
                column: "skill_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_UserEmployerId",
                schema: "carevo",
                table: "projects",
                column: "UserEmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_UserId",
                schema: "carevo",
                table: "projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_contacts_UserId",
                schema: "carevo",
                table: "user_contacts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_user_employers_EmployerId",
                schema: "carevo",
                table: "user_employers",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "ix_user_employers_userid_employerid",
                schema: "carevo",
                table: "user_employers",
                columns: new[] { "UserId", "EmployerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_skills_SkillId",
                schema: "carevo",
                table: "user_skills",
                column: "SkillId");
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
                name: "user_employers",
                schema: "carevo");

            migrationBuilder.DropTable(
                name: "employers",
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
                name: "employerseq",
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
                name: "userempseq",
                schema: "carevo");

            migrationBuilder.DropSequence(
                name: "userseq",
                schema: "carevo");
        }
    }
}
