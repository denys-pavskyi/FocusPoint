using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusPoint.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneToOneLinkBetweenUserAndUserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserSettingId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSettingId",
                table: "Users",
                column: "UserSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserSettings_UserSettingId",
                table: "Users",
                column: "UserSettingId",
                principalTable: "UserSettings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserSettings_UserSettingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserSettingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserSettingId",
                table: "Users");
        }
    }
}
