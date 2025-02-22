using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceChat.DataEntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFieldMessageNameInTableMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Messages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Messages",
                type: "text",
                nullable: true);
        }
    }
}
