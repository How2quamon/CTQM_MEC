using CTQM_Car.Data;

namespace CTQM_MEC.Models
{
    public class ShopModelView
    {
        public List<Xe>? ListXe { get; set; }
        public Xe? XeDetails { get; set; }
        public ThongTinChiTietXe? MoreInfo { get; set; }
        public int? SoLuongTT { get; set; }
        public int? MaKhachHang { get; set; }
        public int? SoLuongMua { get; set; }
        public double? GiaThanh { get; set; }
        public int? MaXe { get; set; }
    }
}
