﻿// Copyright (c) Adam Mytton. All Rights Reserved.

namespace AmCalendar.Persistence.Migrations
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Migrations for the initial creation of the database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// The upgrade migration.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AmCalendar");

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                schema: "AmCalendar",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    CreateRequestId = table.Column<Guid>(nullable: false),
                    Summary = table.Column<string>(maxLength: 255, nullable: false),
                    Location = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    EndDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_CreateRequestId",
                schema: "AmCalendar",
                table: "CalendarEvents",
                column: "CreateRequestId",
                unique: true);
        }

        /// <summary>
        /// The downgrade migration.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEvents",
                schema: "AmCalendar");
        }
    }
}
