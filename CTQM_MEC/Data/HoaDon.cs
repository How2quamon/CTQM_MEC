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
        public int MaGiaoDich { get; set; }
        [ForeignKey("MaGiaoDich")]
        public GiaoDich? GiaoDich { get; set; }
        [Required]
        public DateTime NgayThanhToan { get; set; }
        public string? PhuongThucThanhToan { get; set; }
        [Required]
        public int TongSoLuong { get; set; }
        [Required]
        public double ThanhTien { get; set; }
    }
}
