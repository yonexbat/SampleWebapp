CREATE DATABASE test;
\connect test;

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "GuestBookEntry" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Text" text NULL,
    CONSTRAINT "PK_GuestBookEntry" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210626104025_InitialCreate', '5.0.7');

COMMIT;

