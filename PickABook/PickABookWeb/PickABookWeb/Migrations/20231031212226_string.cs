using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PickABookWeb.Migrations
{
    public partial class @string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Books");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
