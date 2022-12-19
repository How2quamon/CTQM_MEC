using CTQM_Car.Data;

namespace CTQM_MEC.Models
{
    public class ProfileModelView
    {
        public int? MaKhachHang { get; set; }
        public string? TenKhachHang { get; set; }
        public List<Xe>? ListXe { get; set; }
        public string? newName { get; set; }
        public string? newPhone { get; set; }
        public DateTime? newBirthDay { get; set; }
        public string? newAddress { get; set; }
        public string? newBangLai { get; set; }
        public string? oldPass { get; set; }
        public string? newPass { get; set; }
    }
}
