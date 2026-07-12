// ─────────────────────────────────────────────────────────────────────────────
//  ENTITY FRAMEWORK CORE MIGRATIONS
//
//  This folder will be populated by the EF Core tooling.
//  Run the commands below from the BakeAndCake.Api project directory.
// ─────────────────────────────────────────────────────────────────────────────
//
//  PREREQUISITES
//  ─────────────
//  Install the EF Core global tools if you haven't already:
//
//      dotnet tool install --global dotnet-ef
//
//  CREATING THE INITIAL MIGRATION
//  ────────────────────────────────
//  From the repo root (where BakeAndCake.sln lives):
//
//      dotnet ef migrations add InitialCreate \
//          --project BakeAndCake.Api \
//          --startup-project BakeAndCake.Api \
//          --output-dir Migrations
//
//  APPLYING TO THE DATABASE
//  ─────────────────────────
//      dotnet ef database update \
//          --project BakeAndCake.Api \
//          --startup-project BakeAndCake.Api
//
//  REVERTING A MIGRATION
//  ──────────────────────
//      dotnet ef database update PreviousMigrationName \
//          --project BakeAndCake.Api
//
//  REMOVING THE LAST UNAPPLIED MIGRATION
//  ──────────────────────────────────────
//      dotnet ef migrations remove \
//          --project BakeAndCake.Api
//
//  GENERATING A SQL SCRIPT (useful for production deploys)
//  ─────────────────────────────────────────────────────────
//      dotnet ef migrations script \
//          --project BakeAndCake.Api \
//          --output migrations.sql \
//          --idempotent
//
// ─────────────────────────────────────────────────────────────────────────────

namespace BakeAndCake.Api.Data.Migrations;

// Migrations are auto-generated — do not hand-edit them.
