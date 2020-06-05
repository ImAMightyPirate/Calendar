using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmHaulage.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AmHaulage");

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                schema: "AmHaulage",
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
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_CreateRequestId",
                schema: "AmHaulage",
                table: "CalendarEvents",
                column: "CreateRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarEvents",
                schema: "AmHaulage");
        }
    }
}
