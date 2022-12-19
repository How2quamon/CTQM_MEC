using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CTQM_Car.Data
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int MaHoaDon { get; set; }
        [Required]
        public int MaKhachHang { get; set; }
        [ForeignKey("MaKhachHang")]
        public KhachHang? KhachHang { set; get; }
        [Required]
        public int MaXe { get; set; }
        [ForeignKey("MaXe")]
        public Xe? Xe { get; set; }
        [Required]
        public DateTime NgayThanhToan { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        [Required]
        public int TongSoLuong { get; set; }
        [Required]
        public double ThanhTien { get; set; }
    }
}
