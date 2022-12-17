using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTQM_MEC.Migrations
{
    public partial class ctqmdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GiayPhepLaiXe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    MaXe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenXe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HangXe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhanKhuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiDongCo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaThanh = table.Column<double>(type: "float", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.MaXe);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    MaGiaoDich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    MaXe = table.Column<int>(type: "int", nullable: false),
                    SoLuongMua = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.MaGiaoDich);
                    table.ForeignKey(
                        name: "FK_GiaoDich_KhachHang_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "MaKhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiaoDich_Xe_MaXe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinChiTietXe",
                columns: table => new
                {
                    MaThongTin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaXe = table.Column<int>(type: "int", nullable: false),
                    Head1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Head2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Head3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinChiTietXe", x => x.MaThongTin);
                    table.ForeignKey(
                        name: "FK_ThongTinChiTietXe_Xe_MaXe",
                        column: x => x.MaXe,
                        principalTable: "Xe",
                        principalColumn: "MaXe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGiaoDich = table.Column<int>(type: "int", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_GiaoDich_MaGiaoDich",
                        column: x => x.MaGiaoDich,
                        principalTable: "GiaoDich",
                        principalColumn: "MaGiaoDich",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_MaKhachHang",
                table: "GiaoDich",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_MaXe",
                table: "GiaoDich",
                column: "MaXe");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaGiaoDich",
                table: "HoaDon",
                column: "MaGiaoDich");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinChiTietXe_MaXe",
                table: "ThongTinChiTietXe",
                column: "MaXe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "ThongTinChiTietXe");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "Xe");
        }
    }
}
