using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayEd.Data.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_User_IdId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_AspNetUsers_User_IdId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Streams_Stream_Id",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Streams_AspNetUsers_User_IdId",
                table: "Streams");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_Stream_Id",
                table: "Incomes");

            migrationBuilder.RenameColumn(
                name: "User_IdId",
                table: "Streams",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Streams_User_IdId",
                table: "Streams",
                newName: "IX_Streams_UserId");

            migrationBuilder.RenameColumn(
                name: "User_IdId",
                table: "Incomes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Incomes_User_IdId",
                table: "Incomes",
                newName: "IX_Incomes_UserId");

            migrationBuilder.RenameColumn(
                name: "User_IdId",
                table: "Budgets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_User_IdId",
                table: "Budgets",
                newName: "IX_Budgets_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "User_Id",
                table: "Streams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Stream_Id1",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "User_Id",
                table: "Incomes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "Budget_Id",
                table: "Expenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "User_Id",
                table: "Budgets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_Stream_Id1",
                table: "Incomes",
                column: "Stream_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_UserId",
                table: "Budgets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_AspNetUsers_UserId",
                table: "Incomes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Streams_Stream_Id1",
                table: "Incomes",
                column: "Stream_Id1",
                principalTable: "Streams",
                principalColumn: "Stream_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Streams_AspNetUsers_UserId",
                table: "Streams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_AspNetUsers_UserId",
                table: "Budgets");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_AspNetUsers_UserId",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Incomes_Streams_Stream_Id1",
                table: "Incomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Streams_AspNetUsers_UserId",
                table: "Streams");

            migrationBuilder.DropIndex(
                name: "IX_Incomes_Stream_Id1",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Streams");

            migrationBuilder.DropColumn(
                name: "Stream_Id1",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "Budget_Id",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Budgets");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Streams",
                newName: "User_IdId");

            migrationBuilder.RenameIndex(
                name: "IX_Streams_UserId",
                table: "Streams",
                newName: "IX_Streams_User_IdId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Incomes",
                newName: "User_IdId");

            migrationBuilder.RenameIndex(
                name: "IX_Incomes_UserId",
                table: "Incomes",
                newName: "IX_Incomes_User_IdId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Budgets",
                newName: "User_IdId");

            migrationBuilder.RenameIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                newName: "IX_Budgets_User_IdId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_Stream_Id",
                table: "Incomes",
                column: "Stream_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_AspNetUsers_User_IdId",
                table: "Budgets",
                column: "User_IdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_AspNetUsers_User_IdId",
                table: "Incomes",
                column: "User_IdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Incomes_Streams_Stream_Id",
                table: "Incomes",
                column: "Stream_Id",
                principalTable: "Streams",
                principalColumn: "Stream_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Streams_AspNetUsers_User_IdId",
                table: "Streams",
                column: "User_IdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
