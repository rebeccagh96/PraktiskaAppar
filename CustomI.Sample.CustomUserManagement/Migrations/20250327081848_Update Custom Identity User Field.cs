using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomI.Sample.CustomUserManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomIdentityUserField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "CstUserMngt",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                schema: "CstUserMngt",
                table: "User",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "CstUserMngt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Picture",
                schema: "CstUserMngt",
                table: "User");
        }
    }
}
