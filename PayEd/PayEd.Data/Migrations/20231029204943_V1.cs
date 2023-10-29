using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayEd.Data.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Initial_balance",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Schools",
                newName: "TwoFactorEnabled");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Schools",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Schools",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Schools",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Schools",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Schools",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Schools",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Schools",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "School_email",
                table: "Schools",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Schools",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Budgets",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Budgets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "School_email",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "Schools",
                newName: "isDeleted");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Schools",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Schools",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "Initial_balance",
                table: "Budgets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
