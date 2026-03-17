using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupplySync.Migrations
{
    /// <inheritdoc />
    public partial class AddingInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Initiated",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoice",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Submitted",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payment",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Initiated");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoice",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Submitted");
        }
    }
}
