using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace N5User.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Unique Id")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false, comment: "Permission description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, comment: "Unique Id")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeForename = table.Column<string>(type: "text", nullable: false, comment: "Employee Forename"),
                    EmployeeSurname = table.Column<string>(type: "text", nullable: false, comment: "Employee Surename"),
                    PermissionTypeId = table.Column<int>(type: "integer", nullable: false, comment: "Permission Type"),
                    PermissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Permission granted on date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_PermissionTypes_PermissionTypeId",
                        column: x => x.PermissionTypeId,
                        principalTable: "PermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Manager" },
                    { 2, "Admin" },
                    { 3, "Sales" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "EmployeeForename", "EmployeeSurname", "PermissionDate", "PermissionTypeId" },
                values: new object[,]
                {
                    { 1, "Michael", "Scott", new DateTime(2024, 3, 31, 22, 47, 37, 610, DateTimeKind.Utc).AddTicks(6480), 1 },
                    { 2, "Pam", "Beasly", new DateTime(2024, 3, 31, 22, 47, 37, 610, DateTimeKind.Utc).AddTicks(6520), 2 },
                    { 3, "Jim", "Halpert", new DateTime(2024, 3, 31, 22, 47, 37, 610, DateTimeKind.Utc).AddTicks(6520), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypeId",
                table: "Permissions",
                column: "PermissionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionTypes");
        }
    }
}
