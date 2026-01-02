CREATE EXTENSION IF NOT EXISTS postgis;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'carevo') THEN
            CREATE SCHEMA carevo;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'carevo') THEN
            CREATE SCHEMA carevo;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE EXTENSION IF NOT EXISTS postgis;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.applicationhistoryseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.applicationseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.contactseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.employerseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.projectseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.skillseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.streakseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.userempseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE SEQUENCE carevo.userseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.employers (
        "Id" integer NOT NULL,
        "Company" varchar(500) NOT NULL,
        "Logo" varchar,
        "Url" varchar,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_employments_Id" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.skills (
        id integer NOT NULL,
        code varchar(100) NOT NULL,
        description varchar NOT NULL,
        effective_date timestamptz NOT NULL,
        created_at timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        created_by varchar(500),
        CONSTRAINT pk_skills_id PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.users (
        "Id" integer NOT NULL,
        "FirstName" varchar(200) NOT NULL,
        "LastName" varchar(200) NOT NULL,
        "Dob" timestamptz NOT NULL,
        "Status" smallint NOT NULL,
        "Address_Line1" varchar,
        "Address_Line2" varchar,
        "Address_Suburb" varchar(200),
        "Address_City" varchar(150),
        "Address_State" varchar(200),
        "Address_PostCode" varchar(20),
        "Address_Country" varchar(100),
        "Address_Coordinates" point,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_users_Id" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.activity_streaks (
        "Id" integer NOT NULL,
        "ActivityDate" date NOT NULL,
        "ApplicationCount" integer NOT NULL,
        "ConsecutiveDayCount" integer,
        "UserId" integer NOT NULL,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" text,
        "VersionStamp" bigint NOT NULL,
        CONSTRAINT "pk_activity_streaks_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_activity_streaks_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.applications (
        "Id" integer NOT NULL,
        "JobId" integer NOT NULL,
        "Status" varchar(50) NOT NULL,
        "AppliedDate" timestamptz NOT NULL,
        "Notes" varchar,
        "PersonalizedEmploymentData" text,
        "UserId" integer NOT NULL,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_applications_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_applications_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.user_contacts (
        "Id" integer NOT NULL,
        "Type" varchar(50) NOT NULL,
        "Value" varchar(500) NOT NULL,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        "VersionStamp" bigint NOT NULL,
        "UserId" integer NOT NULL,
        CONSTRAINT pk_user_contacts_id PRIMARY KEY ("Id"),
        CONSTRAINT fk_user_contacts_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.user_employers (
        "Id" integer NOT NULL,
        "UserId" integer NOT NULL,
        "EmployerId" integer NOT NULL,
        "StartDate" timestamptz NOT NULL,
        "EndDate" timestamptz,
        CONSTRAINT "pk_useremployers_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_user_employers_employers FOREIGN KEY ("EmployerId") REFERENCES carevo.employers ("Id") ON DELETE CASCADE,
        CONSTRAINT fk_user_employers_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.user_skills (
        "UserId" integer NOT NULL,
        "SkillId" integer NOT NULL,
        CONSTRAINT pk_user_skills PRIMARY KEY ("UserId", "SkillId"),
        CONSTRAINT fk_user_skills_skills FOREIGN KEY ("SkillId") REFERENCES carevo.skills (id) ON DELETE CASCADE,
        CONSTRAINT fk_user_skills_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.application_status_history (
        "Id" integer NOT NULL,
        "Status" varchar(50) NOT NULL,
        "PreviousStatus" varchar(50),
        "StatusChangedDate" timestamptz NOT NULL,
        "Notes" varchar,
        "ChangedBy" varchar(500),
        "Reason" varchar(500),
        "ApplicationId" integer NOT NULL,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" text,
        "VersionStamp" bigint NOT NULL,
        CONSTRAINT "pk_application_status_history_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_application_status_history_applications FOREIGN KEY ("ApplicationId") REFERENCES carevo.applications ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.projects (
        "Id" integer NOT NULL,
        "Title" varchar(500) NOT NULL,
        "Description" varchar NOT NULL,
        "UserEmployerId" integer,
        "UserId" integer,
        "CreatedAt" timestamptz NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_projects_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_projects_useremployers FOREIGN KEY ("UserEmployerId") REFERENCES carevo.user_employers ("Id") ON DELETE SET NULL,
        CONSTRAINT fk_projects_users_individual FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE TABLE carevo.project_skills (
        project_id integer NOT NULL,
        skill_id integer NOT NULL,
        CONSTRAINT pk_project_skills PRIMARY KEY (project_id, skill_id),
        CONSTRAINT fk_project_skills_projects FOREIGN KEY (project_id) REFERENCES carevo.projects ("Id") ON DELETE CASCADE,
        CONSTRAINT fk_project_skills_skills FOREIGN KEY (skill_id) REFERENCES carevo.skills (id) ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX idx_activity_streaks_user_consecutive ON carevo.activity_streaks ("UserId", "ConsecutiveDayCount");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE UNIQUE INDEX idx_activity_streaks_user_date ON carevo.activity_streaks ("UserId", "ActivityDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX idx_app_history_appid_date ON carevo.application_status_history ("ApplicationId", "StatusChangedDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_applications_UserId" ON carevo.applications ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_project_skills_skill_id" ON carevo.project_skills (skill_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_projects_UserEmployerId" ON carevo.projects ("UserEmployerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_projects_UserId" ON carevo.projects ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_user_contacts_UserId" ON carevo.user_contacts ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_user_employers_EmployerId" ON carevo.user_employers ("EmployerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE UNIQUE INDEX ix_user_employers_userid_employerid ON carevo.user_employers ("UserId", "EmployerId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    CREATE INDEX "IX_user_skills_SkillId" ON carevo.user_skills ("SkillId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260102150949_UMRMigration_Initial_1_0_11_20260103020943') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260102150949_UMRMigration_Initial_1_0_11_20260103020943', '10.0.0');
    END IF;
END $EF$;
COMMIT;

