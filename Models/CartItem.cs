namespace BaoCaoTTCM.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Anh { get; set; }
        public string Size { get; set; }
        public float Gia { get; set; }
        public int SoLuong { get; set; }
        public float Total => Gia * SoLuong;
    }
}
