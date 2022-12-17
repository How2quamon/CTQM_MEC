namespace CTQM_MEC.Models
{
    public class VMLogin
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool KeepLogedIn { get; set; }
        public string? NewEmail { get; set; }
        public string? NewPassword { get; set; }
        public string? NewName { get; set; }
        public string? NewPhone { get; set; }
    }
}
