using CTQM_Car.Data;

namespace CTQM_MEC.Models
{
    public class ProfileModelView
    {
        public int? MaKhachHang { get; set; }
        public string? TenKhachHang { get; set; }
        public List<Xe>? ListXe { get; set; }
    }
}
