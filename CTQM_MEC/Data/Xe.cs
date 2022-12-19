using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CTQM_Car.Data
{
    [Table("Xe")]
    public class Xe
    {
        [Key]
        public int MaXe { get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenXe { get; set; }
        [Required]
        public string? HangXe { get; set; }
        [Required]
        public string? PhanKhuc { get; set; }
        [Required]
        public string? LoaiDongCo { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double GiaThanh { get; set; }
        public string? MoTa { get; set; }
        public string? Head1 { get; set; }
        public string? MoTa2 { get; set; }

    }
}
