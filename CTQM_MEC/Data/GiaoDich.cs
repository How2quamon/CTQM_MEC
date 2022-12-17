using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTQM_Car.Data
{
    [Table("GiaoDich")]
    public class GiaoDich
    {
        [Key]
        public int MaGiaoDich { get; set; }
        [Required]
        public int MaKhachHang { get; set; }
        [ForeignKey("MaKhachHang")]
        public KhachHang? KhachHang { set; get; }
        [Required]
        public int MaXe { get; set; }
        [ForeignKey("MaXe")]
        public Xe? Xe { get; set; }
        [Required]
        public int SoLuongMua { get; set; }
        public double TongTien { get; set; }
    }
        
}
