using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CTQM_Car.Data
{
    [Table("ThongTinChiTietXe")]
    public class ThongTinChiTietXe
    {
        [Key]
        public int MaThongTin { get; set; }
        [Required]
        public int MaXe { get; set; }
        [ForeignKey("MaXe")]
        public Xe? Xe { get; set; }
        [Required]
        public string? Head1 { get; set; }
        [Required]
        public string? Title1 { get; set; }
        public string? Head2 { get; set; }
        public string? Title2 { get; set; }
        public string? Head3 { get; set; }
        public string? Title3 { get; set; }

    }
}
