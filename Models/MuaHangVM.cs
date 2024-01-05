namespace BaoCaoTTCM.Models
{
    public class MuaHangVM
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int TinhThanh { get; set; }
        public int QuanHuyen { get; set; }
        public int PhuongXa { get; set; }
        public int PaymentID { get; set; }
        public string Note { get; set; }
    }
}
