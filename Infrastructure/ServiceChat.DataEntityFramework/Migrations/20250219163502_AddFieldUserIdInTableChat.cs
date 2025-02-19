using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceChat.DataEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldUserIdInTableChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserIds",
                table: "Chats",
                newName: "FriendIds");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "FriendIds",
                table: "Chats",
                newName: "UserIds");
        }
    }
}
