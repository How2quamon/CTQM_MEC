using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTQM_MEC.Migrations
{
    public partial class ctqmdb3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Head1",
                table: "Xe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoTa2",
                table: "Xe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Head1",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "MoTa2",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "KhachHang");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "KhachHang");
        }
    }
}
