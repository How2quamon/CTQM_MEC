using CTQM_Car.Data;

namespace CTQM_MEC.Models
{
    public class CartModelView
    {
        public int? MaKhachHang { get; set; }
        public List<Xe>? ListXe { get; set; }
        public double? TongTien { get; set; }
    }
}
