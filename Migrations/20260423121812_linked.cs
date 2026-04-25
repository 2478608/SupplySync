using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplySync.Migrations
{
    /// <inheritdoc />
    public partial class linked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "VendorApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vendor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UserId",
                table: "Vendor",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_User_UserId",
                table: "Vendor",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_User_UserId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_UserId",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VendorApplication");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vendor");
        }
    }
}
