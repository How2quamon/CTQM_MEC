using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CTQM_Car.Data
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        public int MaKhachHang { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenKhachHang { get; set; }
        [Required]
        [Phone]
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
        [Required]
        public DateTime NgaySinh { get; set; }
        [Required]
        public string? GiayPhepLaiXe { get; set; }
    }
}
