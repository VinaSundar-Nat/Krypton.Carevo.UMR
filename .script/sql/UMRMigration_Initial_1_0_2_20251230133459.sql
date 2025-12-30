CREATE EXTENSION IF NOT EXISTS postgis;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'carevo') THEN
            CREATE SCHEMA carevo;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'carevo') THEN
            CREATE SCHEMA carevo;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE EXTENSION IF NOT EXISTS postgis;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.applicationhistoryseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.applicationseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.contactseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.employmentseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.projectseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.skillseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.streakseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE SEQUENCE carevo.userseq AS integer START WITH 1 INCREMENT BY 1 NO CYCLE;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.skills (
        id integer NOT NULL,
        code varchar(100) NOT NULL,
        description varchar NOT NULL,
        effective_date timestamp NOT NULL,
        created_at timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        created_by varchar(500),
        CONSTRAINT pk_skills_id PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.users (
        "Id" integer NOT NULL,
        "FirstName" varchar(200) NOT NULL,
        "LastName" varchar(200) NOT NULL,
        "Dob" timestamp NOT NULL,
        "Status" smallint NOT NULL,
        "Address_Line1" varchar,
        "Address_Line2" varchar,
        "Address_Suburb" varchar(200),
        "Address_City" varchar(150),
        "Address_State" varchar(200),
        "Address_PostCode" varchar(20),
        "Address_Country" varchar(100),
        "Address_Coordinates" point,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_Users_Id" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.activity_streaks (
        "Id" integer NOT NULL,
        "ActivityDate" date NOT NULL,
        "ApplicationCount" integer NOT NULL,
        "ConsecutiveDayCount" integer,
        "UserId" integer NOT NULL,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" text,
        "VersionStamp" bigint NOT NULL,
        CONSTRAINT "pk_activity_streaks_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_activity_streaks_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.applications (
        "Id" integer NOT NULL,
        "JobId" integer NOT NULL,
        "Status" varchar(50) NOT NULL,
        "AppliedDate" timestamp NOT NULL,
        "Notes" varchar,
        "PersonalizedEmploymentData" text,
        "UserId" integer NOT NULL,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_applications_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_applications_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.employments (
        "Id" integer NOT NULL,
        "Company" varchar(500) NOT NULL,
        "StartDate" timestamp NOT NULL,
        "EndDate" timestamp,
        "Logo" varchar,
        "Url" varchar,
        "UserId" integer NOT NULL,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_employments_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_employments_users FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.user_contacts (
        id integer NOT NULL,
        type varchar(50) NOT NULL,
        value varchar(500) NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "CreatedBy" text,
        "VersionStamp" bigint NOT NULL,
        user_id integer NOT NULL,
        CONSTRAINT pk_user_contacts_id PRIMARY KEY (id),
        CONSTRAINT fk_user_contacts_users FOREIGN KEY (user_id) REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.user_skills (
        user_id integer NOT NULL,
        skill_id integer NOT NULL,
        CONSTRAINT pk_user_skills PRIMARY KEY (user_id, skill_id),
        CONSTRAINT fk_user_skills_skills FOREIGN KEY (skill_id) REFERENCES carevo.skills (id) ON DELETE CASCADE,
        CONSTRAINT fk_user_skills_users FOREIGN KEY (user_id) REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.application_status_history (
        "Id" integer NOT NULL,
        "Status" varchar(50) NOT NULL,
        "PreviousStatus" varchar(50),
        "StatusChangedDate" timestamp NOT NULL,
        "Notes" varchar,
        "ChangedBy" varchar(500),
        "Reason" varchar(500),
        "ApplicationId" integer NOT NULL,
        "ApplicationId1" integer,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" text,
        "VersionStamp" bigint NOT NULL,
        CONSTRAINT "pk_application_status_history_Id" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_application_status_history_applications_ApplicationId1" FOREIGN KEY ("ApplicationId1") REFERENCES carevo.applications ("Id"),
        CONSTRAINT fk_application_status_history_applications FOREIGN KEY ("ApplicationId") REFERENCES carevo.applications ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE TABLE carevo.projects (
        "Id" integer NOT NULL,
        "Title" varchar(500) NOT NULL,
        "Description" varchar NOT NULL,
        "EmploymentId" integer,
        "UserId" integer,
        "CreatedAt" timestamp NOT NULL DEFAULT (CURRENT_TIMESTAMP),
        "CreatedBy" varchar(500),
        CONSTRAINT "pk_projects_Id" PRIMARY KEY ("Id"),
        CONSTRAINT fk_projects_employments FOREIGN KEY ("EmploymentId") REFERENCES carevo.employments ("Id") ON DELETE SET NULL,
        CONSTRAINT fk_projects_users_individual FOREIGN KEY ("UserId") REFERENCES carevo.users ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
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
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX idx_activity_streaks_user_consecutive ON carevo.activity_streaks ("UserId", "ConsecutiveDayCount");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE UNIQUE INDEX idx_activity_streaks_user_date ON carevo.activity_streaks ("UserId", "ActivityDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX idx_app_history_appid_date ON carevo.application_status_history ("ApplicationId", "StatusChangedDate");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_application_status_history_ApplicationId1" ON carevo.application_status_history ("ApplicationId1");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_applications_UserId" ON carevo.applications ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_employments_UserId" ON carevo.employments ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_project_skills_skill_id" ON carevo.project_skills (skill_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_projects_EmploymentId" ON carevo.projects ("EmploymentId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_projects_UserId" ON carevo.projects ("UserId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_user_contacts_user_id" ON carevo.user_contacts (user_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    CREATE INDEX "IX_user_skills_skill_id" ON carevo.user_skills (skill_id);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20251230023502_UMRMigration_Initial_1_0_2_20251230133459') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20251230023502_UMRMigration_Initial_1_0_2_20251230133459', '10.0.0');
    END IF;
END $EF$;
COMMIT;

