using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTQM_MEC.Migrations
{
    public partial class ctqmdb4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_GiaoDich_MaGiaoDich",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "MaGiaoDich",
                table: "HoaDon",
                newName: "MaXe");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_MaGiaoDich",
                table: "HoaDon",
                newName: "IX_HoaDon_MaXe");

            migrationBuilder.AddColumn<int>(
                name: "MaKhachHang",
                table: "HoaDon",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKhachHang",
                table: "HoaDon",
                column: "MaKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHang",
                table: "HoaDon",
                column: "MaKhachHang",
                principalTable: "KhachHang",
                principalColumn: "MaKhachHang",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_Xe_MaXe",
                table: "HoaDon",
                column: "MaXe",
                principalTable: "Xe",
                principalColumn: "MaXe",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_MaKhachHang",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_Xe_MaXe",
                table: "HoaDon");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_MaKhachHang",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "MaKhachHang",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "MaXe",
                table: "HoaDon",
                newName: "MaGiaoDich");

            migrationBuilder.RenameIndex(
                name: "IX_HoaDon_MaXe",
                table: "HoaDon",
                newName: "IX_HoaDon_MaGiaoDich");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_GiaoDich_MaGiaoDich",
                table: "HoaDon",
                column: "MaGiaoDich",
                principalTable: "GiaoDich",
                principalColumn: "MaGiaoDich",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
