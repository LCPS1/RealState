using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealState.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPrpértyImagess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "PropertyImages",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "Descrption",
                table: "PropertyImages",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "File",
                table: "PropertyImages",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PropertyImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PropertyImages");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "PropertyImages",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PropertyImages",
                newName: "Descrption");

            migrationBuilder.AlterColumn<byte[]>(
                name: "File",
                table: "PropertyImages",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
