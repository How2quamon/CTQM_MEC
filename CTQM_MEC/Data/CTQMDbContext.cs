using CTQM_Car.Data;
using Microsoft.EntityFrameworkCore;


namespace CTQM_MEC.Data
{
    public class CTQMDbContext : DbContext
    {
        public CTQMDbContext(DbContextOptions<CTQMDbContext> options) : base(options) { }
        public DbSet<GiaoDich>? Giaodichs { get; set; }
        public DbSet<KhachHang>? KhachHangs { get; set; }
        public DbSet<HoaDon>? HoaDons { get; set; }
        public DbSet<ThongTinChiTietXe>? ThongTinChiTietXes { get; set; }
        public DbSet<Xe>? Xe { get; set; }
    }
}
