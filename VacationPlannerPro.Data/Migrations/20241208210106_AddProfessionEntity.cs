﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlannerPro.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Leaders");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId",
                table: "Workers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionId",
                table: "Leaders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_ProfessionId",
                table: "Workers",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_ProfessionId",
                table: "Leaders",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaders_Professions_ProfessionId",
                table: "Leaders",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Professions_ProfessionId",
                table: "Workers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaders_Professions_ProfessionId",
                table: "Leaders");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Professions_ProfessionId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropIndex(
                name: "IX_Workers_ProfessionId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Leaders_ProfessionId",
                table: "Leaders");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Leaders");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Workers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Leaders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
