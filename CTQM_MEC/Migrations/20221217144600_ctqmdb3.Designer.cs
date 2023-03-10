// <auto-generated />
using System;
using CTQM_MEC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CTQM_MEC.Migrations
{
    [DbContext(typeof(CTQMDbContext))]
    [Migration("20221217144600_ctqmdb3")]
    partial class ctqmdb3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CTQM_Car.Data.GiaoDich", b =>
                {
                    b.Property<int>("MaGiaoDich")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaGiaoDich"), 1L, 1);

                    b.Property<int>("MaKhachHang")
                        .HasColumnType("int");

                    b.Property<int>("MaXe")
                        .HasColumnType("int");

                    b.Property<int>("SoLuongMua")
                        .HasColumnType("int");

                    b.Property<double>("TongTien")
                        .HasColumnType("float");

                    b.HasKey("MaGiaoDich");

                    b.HasIndex("MaKhachHang");

                    b.HasIndex("MaXe");

                    b.ToTable("GiaoDich");
                });

            modelBuilder.Entity("CTQM_Car.Data.HoaDon", b =>
                {
                    b.Property<int>("MaHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaHoaDon"), 1L, 1);

                    b.Property<int>("MaGiaoDich")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayThanhToan")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhuongThucThanhToan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.Property<int>("TongSoLuong")
                        .HasColumnType("int");

                    b.HasKey("MaHoaDon");

                    b.HasIndex("MaGiaoDich");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("CTQM_Car.Data.KhachHang", b =>
                {
                    b.Property<int>("MaKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaKhachHang"), 1L, 1);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GiayPhepLaiXe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaKhachHang");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("CTQM_Car.Data.ThongTinChiTietXe", b =>
                {
                    b.Property<int>("MaThongTin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaThongTin"), 1L, 1);

                    b.Property<string>("Head1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Head2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Head3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaXe")
                        .HasColumnType("int");

                    b.Property<string>("Title1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title3")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaThongTin");

                    b.HasIndex("MaXe");

                    b.ToTable("ThongTinChiTietXe");
                });

            modelBuilder.Entity("CTQM_Car.Data.Xe", b =>
                {
                    b.Property<int>("MaXe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaXe"), 1L, 1);

                    b.Property<double>("GiaThanh")
                        .HasColumnType("float");

                    b.Property<string>("HangXe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Head1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoaiDongCo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhanKhuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenXe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaXe");

                    b.ToTable("Xe");
                });

            modelBuilder.Entity("CTQM_Car.Data.GiaoDich", b =>
                {
                    b.HasOne("CTQM_Car.Data.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("MaKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CTQM_Car.Data.Xe", "Xe")
                        .WithMany()
                        .HasForeignKey("MaXe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhachHang");

                    b.Navigation("Xe");
                });

            modelBuilder.Entity("CTQM_Car.Data.HoaDon", b =>
                {
                    b.HasOne("CTQM_Car.Data.GiaoDich", "GiaoDich")
                        .WithMany()
                        .HasForeignKey("MaGiaoDich")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GiaoDich");
                });

            modelBuilder.Entity("CTQM_Car.Data.ThongTinChiTietXe", b =>
                {
                    b.HasOne("CTQM_Car.Data.Xe", "Xe")
                        .WithMany()
                        .HasForeignKey("MaXe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Xe");
                });
#pragma warning restore 612, 618
        }
    }
}
