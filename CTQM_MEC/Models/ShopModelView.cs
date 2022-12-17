using CTQM_Car.Data;

namespace CTQM_MEC.Models
{
    public class ShopModelView
    {
        public List<Xe>? ListXe { get; set; }
        public Xe? XeDetails { get; set; }
        public ThongTinChiTietXe? MoreInfo { get; set; }
    }
}
