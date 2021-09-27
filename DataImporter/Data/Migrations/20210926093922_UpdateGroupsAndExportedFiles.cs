using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Data.Migrations
{
    public partial class UpdateGroupsAndExportedFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportedFiles_Groups_GroupId",
                table: "ExportedFiles");

            migrationBuilder.DropIndex(
                name: "IX_ExportedFiles_GroupId",
                table: "ExportedFiles");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "ExportedFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExportedFiles");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ExportedFiles",
                newName: "impotedFileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "impotedFileId",
                table: "ExportedFiles",
                newName: "GroupId");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "ExportedFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ExportedFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExportedFiles_GroupId",
                table: "ExportedFiles",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportedFiles_Groups_GroupId",
                table: "ExportedFiles",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
